using Microsoft.AspNetCore.Mvc;
using phoneApi.Models.Domain;
using phoneApi.Models.Res;
using phoneApi.ViewModels;

namespace phoneApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IHomeRes home;

        public HomeController(IHomeRes _home)
        {
            home = _home;
        }
        [Route("Products")]
        [HttpGet]
        public IActionResult Index(string item = "", int GenreId = 0, int pg = 1)
        {
            List<Product> products = home.Search(item, GenreId);
            //List<Category> categories = home.GetAllCategory();
            const int pagesize = 8;
            if (pg < 1)
                pg = 1;
            int recscount = products.Count();
            var Pager = new Pager(recscount, pg, pagesize);
            int recskip = (pg - 1) * pagesize;
            var data = products.Skip(recskip).Take(Pager.PageSize).ToList();
            //this.ViewBag.pager = Pager;
            ProductcatagoryViewModelcs vm = new ProductcatagoryViewModelcs
            {
                Products = data,
                
                //categories = categories,
                //item = item,
                //GenreId = GenreId

            };


            return Ok(vm);
        }
    }
}