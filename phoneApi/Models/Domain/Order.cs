using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace phoneApi.Models.Domain
{
    public class Order
    {
            public int Id { get; set; }
            [Required]
            public string UserId { get; set; }
            public string NameUser { get; set; }
            public DateTime CreateDate { get; set; } = DateTime.UtcNow;
            [Required]
            public int OrderStatusId { get; set; }
            public bool IsDeleted { get; set; } = false;
          // [JsonIgnore]
            public OrderStatus OrderStatuses { get; set; }
          // [JsonIgnore]
            public List<OrderDetail>? OrderDetails { get; set; }
        }
    }


