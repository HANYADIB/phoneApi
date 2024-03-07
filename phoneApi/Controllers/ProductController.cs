using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using phoneApi.Mapping;
using phoneApi.Models.Domain;
using phoneApi.Models.Dto;
using phoneApi.Models.DTO;
using phoneApi.Models.Res;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace phoneApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMapper _mapper;
        IHostingEnvironment host;
        ISupplierRes srs;
        IProductRes prs;
        ICatagoryRes cat;
        public ProductController(IProductRes _prs, IHostingEnvironment _host,
            ISupplierRes _srs, ICatagoryRes _cat, IMapper mapper)
        {
            host = _host;
            prs = _prs;
            srs = _srs;
            cat = _cat;
            _mapper = mapper;
        }

        [Route("Index")]
        [HttpGet]
        public IActionResult index(string? item, int catagorgsId, int suppliersId, int pg = 1)
        {
            var pro = prs.Search(item, catagorgsId, suppliersId);
            //var data = _mapper.Map<ICollection<ProductsDto>>(pro);
            var data = pro.Select(c => c.ToProductssDto()).ToList();

            const int pagesize = 8;
            if (pg < 1)
                pg = 1;
            int recscount = data.Count();
            var Pager = new Pager(recscount, pg, pagesize);
            int recskip = (pg - 1) * pagesize;
            var datapage = data.Skip(recskip).Take(Pager.PageSize).ToList();
            return Ok(datapage);

        }
        [HttpGet("Num")]
        public IActionResult Numbersofpages(string? item, int catagorgsId, int suppliersId, int pg = 1)
        {
            var pro = prs.Search(item, catagorgsId, suppliersId);
            //var data = _mapper.Map<ICollection<ProductsDto>>(pro);
            var data = pro.Select(c => c.ToProductssDto()).ToList();
            const int pagesize = 8;
            if (pg < 1)
                pg = 1;
            int recscount = pro.Count();
            var Pager = new Pager(recscount, pg, pagesize);
            int recskip = (pg - 1) * pagesize;
            var datapage = data.Skip(recskip).Take(Pager.PageSize).ToList();
            return Ok(datapage);

        }
        [Route("Details/{Id:int}")]
        [HttpGet]
        public IActionResult Details(int Id)
        {
            var ptd = prs.GetDetails(Id);
            //var data = _mapper.Map<ProductsDto>(ptd);
            var data = new ProductsDto();

            //data.Product_Id = ptd.Product_Id;
            data.Name = ptd.Name;
            data.Price = ptd.Price;
            data.Cat_Id = ptd.Category_Id;
            //data.Cat_Name = ptd.Categorys.Namecat;
            data.Sup_Id = ptd.Supplier_Id;
            return Ok(data);
        }
        [Route("Delete/{Id:int}")]
        [HttpDelete]
        public IActionResult Delete(int Id)
        {
            prs.Delete(Id);

            return Ok();
        }

        [HttpPost("adding")]
        public async Task<IActionResult> Add([FromForm] proDto dept)
        {

            var dpt = prs.GetbyNamebyId(dept.Name);

            var status = new Status();
            if (dpt != null)

            {
                status.StatusCode = 0;
                status.Message = $"product {dept.Name} is already in use.";
                return StatusCode(500, status);
            }
            if (ModelState.IsValid)
            {
                if (dept.File != null)
                {
                    string fileName = string.Empty;
                    string update = Path.Combine(host.WebRootPath, "Images"); ;
                    fileName = dept.File.FileName;
                    string fullpath = Path.Combine(update, fileName);
                    dept.File.CopyTo(new FileStream(fullpath, FileMode.Create));

                    dept.Image = fileName;
                }
                var item = new Product
                {
                    Name = dept.Name,
                    Price = dept.Price,
                    Description = dept.Description,
                    Image = dept.Image,
                    Category_Id = dept.Cat_Id,
                    Supplier_Id = dept.Sup_Id,
                };
                prs.Adding(item);
                string url = Url.Link("GetOneDeptRoute", new { id = item.Product_Id });
                return Created(url, item);
            }
            else { return BadRequest(); }
        }
        //    else { return View("New"); }
        //}
        //public IActionResult Edite(int Id)
        //{
        //    ViewBag.cat = cat.Getall();
        //    ViewData["sup"] = srs.Getall();
        //    Product DEPT2 = prs.Getbyid(Id);

        //    return View(DEPT2);

        //}

        [HttpPut("edite/{id:int}")]
        public IActionResult edite([FromBody] proDto items, int Id)
        {
            var dpt = prs.GetbyNamebyId(items.Name, Id);
            var status = new Status();
            if (dpt != null)

            {
                status.StatusCode = 0;
                status.Message = $"product {items.Name} is already in use.";
                return StatusCode(402, status);
            }

            if (ModelState.IsValid == true)
            {

                prs.Edite(items, Id);
                return StatusCode(204, "succesus");
            }
            else { return BadRequest(); }
        }


        [HttpGet("productscatagorybyId/{Id}")]
        public IActionResult productsbycatageryId(int Id)
        {
            var data = prs.GetproductbycatategoryId(Id);
            return Ok(data);
        }
    }
}