using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using phoneApi.Mapping;
using phoneApi.Models.Domain;
using phoneApi.Models.DTO;
using phoneApi.Models.Res;

namespace phoneApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CataegoryController : ControllerBase
    {
        private readonly ICatagoryRes cat;
        private readonly IMapper _mapper;

        public CataegoryController(ICatagoryRes cat, IMapper mapper)
        {
            this.cat = cat;
            _mapper = mapper;
        }

        [Route("Index")]
        [HttpGet]
        public IActionResult index()
        {
            List<Category> dept = cat.Getall();
            var deptDto = dept.Select(s =>s.ToCatagoryDto()).ToList();
            //CatagoryDto dto = new CatagoryDto();

            //// var data = _mapper.Map<ICollection<CatagoryDto>>(dept);
            
            //    foreach (var itemes in dept)
            //    {
            //        dto.Cat_Id.Add(itemes.Cat_Id);
            //        dto.Namecat.Add(itemes.Namecat);
            //        foreach (var item in itemes.Products)
            //        {
            //            if (itemes.Products != null)
            //            {
            //                dto.NameProduct.Add(item.Name);
            //                dto.id.Add(item.Product_Id);
            //            }

            //        }

            //    } 

            return Ok(deptDto);

        }
        [Route("getbyid")]
        [HttpGet]
        public IActionResult getbyid(int Id)
        {
            Category dept = cat.Getbyid(Id);
            
            var data = _mapper.Map<CatagoryDto>(dept);
            //if (dept == null)
            //{
            //    return NotFound();
            //}
            //CatagoryDto dto = new CatagoryDto();
            //dto.Cat_Id = dept.Cat_Id;
            //dto.Namecat = dept.Namecat;
            //foreach (var item in dept.Products)
            //{
            //    if (dept.Products != null)
            //    {
            //        dto.NameProduct.Add(item.Name);
            //        dto.id.Add(item.Product_Id);
            //    }
            //}
            return Ok(data);

        }
        [HttpPost("Adding")]
        public async Task< IActionResult> Add([FromForm] CatagotyImage dept)
        {
           
            if (ModelState.IsValid)
            {
                using var stream = new MemoryStream();
                await dept.Img.CopyToAsync(stream);
                var item = new Category
                {
                    Namecat = dept.Namecat,

                    Img = stream.ToArray()
                };
                cat.Adding(item);
                string url = Url.Link("GetOneDeptRoute", new { id = item.Cat_Id });
                return Created(url, dept);
            }
            else { return NotFound($"not category"); }
        }
        [Route("edite")]
        [HttpPut]
        public IActionResult edite([FromForm] CatagotyImage items, int Id)
        {
            
            if (ModelState.IsValid == true)
            {

                cat.Edite(items, Id);
                return StatusCode(204, "succesus");
            }
            else { return BadRequest(); }
        }
    }
}
