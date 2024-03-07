using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace phoneApi.Models.Domain
{
    public class CartDetail
    {
        public int Id { get; set; }
        [Required]
        public int ShoppingCartId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double UnitPrice { get; set; }
        public Product Products { get; set; }

        public ShoppingCart ShoppingCarts { get; set; }
    }
}
