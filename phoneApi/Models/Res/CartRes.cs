using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using phoneApi.Models.Domain;
using phoneApi.Models.Dto;
using System.Security.Claims;

namespace phoneApi.Models.Res
{
    public class CartRes: ICartRes
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly DatabaseContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartRes(UserManager<ApplicationUser> userManager, DatabaseContext db,
            IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _db = db;
            _httpContextAccessor = httpContextAccessor;

        }
        [HttpGet]
        private string GetUserId()
        {
            var principal = _httpContextAccessor.HttpContext.User;
            string userId = _userManager.GetUserId(principal);
            return userId;

        }
        public async Task<ShoppingCart> GetUserCart()
        {
            var userId = GetUserId();
            if (userId == null)
                throw new Exception("Invalid userid");
            var shoppingCart = await _db.ShoppingCarts
                                  .Include(a => a.CartDetails)
                                  .ThenInclude(a => a.Products)
                                  .ThenInclude(a => a.Categorys)
                                  .Where(a => a.UserId == userId).FirstOrDefaultAsync();

            return shoppingCart;

        }
        public async Task<ShoppingCart> GetCart(string userId)
        {
            var cart = await _db.ShoppingCarts.FirstOrDefaultAsync(x => x.UserId == userId);
            return cart;
        }
        public async Task<int> GetCartItemCount(string userId = "")
        {
            userId = GetUserId();


            ShoppingCart? mmm = await _db.ShoppingCarts
                                  .Include(a => a.CartDetails)
                                  //.ThenInclude(a => a.Products)
                                  //.ThenInclude(a => a.Categorys)
                                  .Where(a => a.UserId == userId).FirstOrDefaultAsync();
            if (mmm == null) return 0;
            if (!string.IsNullOrEmpty(userId))
                return mmm.CartDetails.Count;
            return 0;

        }
        public async Task<bool> DoCheckout()
        {
            using var transaction = _db.Database.BeginTransaction();
            try
            {
                // move data from cartDetail to order and order detail then we will remove cart detail
                var userId = GetUserId();
                if (string.IsNullOrEmpty(userId))
                    throw new Exception("User is not logged-in");
                var cart = await GetCart(userId);
                if (cart is null)
                    throw new Exception("Invalid cart");
                var cartDetail = _db.CartDetails
                                    .Where(a => a.ShoppingCartId == cart.Id).ToList();
                if (cartDetail.Count == 0)
                    throw new Exception("Cart is empty");
                ApplicationUser usertest = await _userManager.FindByIdAsync(userId);
                Order order = new Order()
                {
                    UserId = userId,
                    NameUser = usertest.UserName,
                    CreateDate = DateTime.UtcNow,
                    OrderStatusId = 1,//pending && 2 Shipping

                };
                _db.Orders.Add(order);
                _db.SaveChanges();
                foreach (var item in cartDetail)
                {
                    var OrderDetail = new OrderDetail
                    {
                        ProductId = item.ProductId,
                        OrderId = order.Id,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice
                    };
                    _db.OrderDetails.Add(OrderDetail);
                }
                _db.SaveChanges();

                // removing the cartdetails
                _db.CartDetails.RemoveRange(cartDetail);
                _db.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public async Task<int> AddItem(int Pro_Id, int qty)
        {
            var status = new Status();
            string userId = GetUserId();
            using var transaction = _db.Database.BeginTransaction();
            try
            {
                if (string.IsNullOrEmpty(userId))
                    throw new Exception("user is not logged-in");
                var cart = await GetCart(userId);
                if (cart is null)
                {
                    cart = new ShoppingCart
                    {
                        UserId = userId
                    };
                    _db.ShoppingCarts.Add(cart);
                }
                _db.SaveChanges();
                // Shopping card for client
                var cartItem = _db.CartDetails
                                  .FirstOrDefault(a => a.ShoppingCartId == cart.Id && a.ProductId == Pro_Id);
                if (cartItem is not null)
                {
                    cartItem.Quantity += qty;
                    status.Message = "product is found in cart";
                }
                else
                {
                    var pro = _db.Products.Find(Pro_Id);
                    cartItem = new CartDetail
                    {
                        ProductId = Pro_Id,
                        ShoppingCartId = cart.Id,
                        Quantity = qty,
                        UnitPrice = pro.Price  
                    };
                    _db.CartDetails.Add(cartItem);
                }
                _db.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
            }
            var cartItemCount = await GetCartItemCount(userId);
            return cartItemCount;
        }
        public async Task<int> RemoveItem(int Pro_Id)
        {
            using var transaction = _db.Database.BeginTransaction();
            string userId = GetUserId();
            try
            {
                if (string.IsNullOrEmpty(userId))
                    throw new Exception("user is not logged-in");
                var cart = await GetCart(userId);
                if (cart is null)
                    throw new Exception("Invalid cart");
                // cart detail section
                var cartItem = _db.CartDetails
                                  .FirstOrDefault(a => a.ShoppingCartId == cart.Id && a.ProductId == Pro_Id);
                if (cartItem is null)
                    throw new Exception("Not items in cart");
                else if (cartItem.Quantity == 1)
                    _db.CartDetails.Remove(cartItem);
                else
                    cartItem.Quantity = cartItem.Quantity - 1;
                _db.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {

            }
            var cartItemCount = await GetCartItemCount(userId);
            return cartItemCount;
        }
        public List<Supplier> GetAllSupplier()
        {
            List<Supplier> sup = _db.Suppliers.ToList();
            return sup;
        }
    }
}
