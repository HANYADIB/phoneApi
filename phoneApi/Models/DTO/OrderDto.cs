using System.ComponentModel.DataAnnotations;

namespace phoneApi.Models.DTO
{
    public class OrderDto
    {
        public int Id { get; set; }
     
        public string UserId { get; set; }
        public string NameUser { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; } = false;
        public List<OrderDetailDto> OrderDetails { get; set; }
       

    }
}
