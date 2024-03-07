using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace phoneApi.Models.Domain
{
    public class OrderDetail
    {
        public int Id { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double UnitPrice { get; set; }
        //[JsonIgnore]
        public Order Orders { get; set; }
        //[JsonIgnore]
        public Product Products { get; set; }
    }
}
