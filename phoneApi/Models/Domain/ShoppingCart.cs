using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace phoneApi.Models.Domain
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        public bool IsDeleted { get; set; } = false;
        [JsonIgnore]
        public ICollection<CartDetail> CartDetails { get; set; }
    }
}
