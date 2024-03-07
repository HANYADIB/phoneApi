using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using phoneApi.Models.Res;
using System.Diagnostics.Eventing.Reader;

namespace phoneApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICartRes _cartRepo;

        public CartController(ICartRes cartRepo)
        {
            _cartRepo = cartRepo;
        }
        [Route("Additem")]
        [HttpPost]
        public async Task<IActionResult> AddItem(int Product_Id, int qty = 1)
        {
            var cartCount = await _cartRepo.AddItem(Product_Id, qty);
            if (cartCount != null)
                return Ok(cartCount);
            return BadRequest("error");
        }
        [Route("RemoveItem")]
        [HttpDelete]
        public async Task<IActionResult> RemoveItem(int Product_Id)
        {
            var cartCount = await _cartRepo.RemoveItem(Product_Id);
            return StatusCode(204,"delete");
        }
        [Route("GetUserCart")]
        [HttpGet]
        public async Task<IActionResult> GetUserCart()
        {
            var cart = await _cartRepo.GetUserCart();
            // ViewBag.sup = _cartRepo.GetAllSupplier();
            return StatusCode(201, cart);

            }
        [Route("GetTotalItemInCart")]
        [HttpGet]
        public async Task<IActionResult> GetTotalItemInCart()
        {
            var cartItem = await _cartRepo.GetCartItemCount();
            return Ok(cartItem);
        }

        //public async Task<IActionResult> Checkout()
        //{
        //    bool isCheckedOut = await _cartRepo.DoCheckout();
        //    if (!isCheckedOut)
        //        throw new Exception("Something happen in server side");
        //    return RedirectToAction("UserOrders", "UserOrder");
        //}
    }
}
