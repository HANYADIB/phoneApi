using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using phoneApi.Mapping;
using phoneApi.Models.Domain;
using phoneApi.Models.DTO;
using phoneApi.Models.Res;

namespace phoneApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        DatabaseContext vvv = new DatabaseContext();
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserOrderRes _userOrderRepo;

        public OrderController(IUserOrderRes userOrderRepo, UserManager<ApplicationUser> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _userOrderRepo = userOrderRepo;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }
        private string GetUserId()
        {
            var principal = _httpContextAccessor.HttpContext.User;
            string userId = _userManager.GetUserId(principal);
            return userId;
        }
        [Route("Order")]
        [HttpGet]
        public async Task<IActionResult> UserOrders()
        {
          
            var orders = await _userOrderRepo.UserOrders();
            var data =  orders.Select(x => x.ToOrderDto()).ToList();
            //var dto = new List<OrderwithdetailsDTO>();
           
            //foreach (var order in orders)
            //{
            //    dto.Add(new OrderwithdetailsDTO()
            //    {
            //        Id = order.Id,
            //        Nameuser = order.NameUser,
            //        createdate = order.CreateDate,
            //        details = new List<OrderdetailDTO>()
            //    });

                
            //}
            return Ok(data);
        }

    }
}