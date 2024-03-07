using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using phoneApi.Models.Domain;
using phoneApi.Models.DTO;

namespace phoneApi.Models.Res
{
    public class UserOrderRes: IUserOrderRes
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly DatabaseContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserOrderRes(UserManager<ApplicationUser> userManager, DatabaseContext db,
            IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IEnumerable<Order>> UserOrders()
        {
            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
                throw new Exception("User is not logged-in");
            var orders = await _db.Orders
                            .Include(x => x.OrderStatuses)
                            .Include(x => x.OrderDetails)
                            .ThenInclude(x => x.Products)
                            .ThenInclude(x => x.Categorys)
                            //.Select(m => new OrderwithdetailsDTO
                            //{
                            //    createdate = m.CreateDate,
                            //    Nameuser = m.NameUser,
                                
                            //    })
                            .Where(a => a.UserId == userId)
                            .ToListAsync();
            //var test = orders.Select(order => new OrderwithdetailsDTO()
            //{
            //    createdate = order.CreateDate,
            //    Nameuser = order.NameUser,
               
            //}).ToList();
           
           // var dto = new List<OrderwithdetailsDTO>();
           //foreach (var order in orders)
           // {
           //     dto.Add(new OrderwithdetailsDTO()
           //     {
           //         Nameuser = order.NameUser,
           //         createdate = order.CreateDate,
           //     }
           //       );
           // }
            //OrderwithdetailsDTO orderDTO = new OrderwithdetailsDTO
            //{
            //foreach (var item in orderDTO)
            //{

            //        createdate = item.CreateDate,
            //        Nameuser = item.NameUsern,

            //    };
            //    foreach (var items in orders.OrderDetails)
            //    {
            //        OrderdetailDTO bbb = new()
            //        {
            //            Quantity = items.Quantity,
            //            UnitPrice = items.unitprice

            //        };
            //        orderDTO.details.Add(bbb);
            //    }
            //}

            return orders;
        }

        private string GetUserId()
        {
            var principal = _httpContextAccessor.HttpContext.User;
            string userId = _userManager.GetUserId(principal);
            return userId;
        }
        public List<Order> GetAllOrder(string Id)
        {
            List<Order> order = _db.Orders.Include(c => c.OrderStatuses).Where(a => a.UserId == Id).ToList();
            return order;
        }
        public int GetNumdersOrder(string Id)
        {
            var order = _db.Orders.Where(a => a.UserId == Id).Count();
            return order;
        }
    }
}
