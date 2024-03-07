using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace phoneApi.Models.Domain
{
    public class OrderStatus
    {
            public int Id { get; set; }
            [Required]
            public int StatusId { get; set; }
            [Required, MaxLength(20)]
            public string? StatusName { get; set; }
        }
    }

