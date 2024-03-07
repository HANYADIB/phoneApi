using phoneApi.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace phoneApi.Models.DTO
{
    public class OrderwithdetailsDTO
    {
        //public string UserId { get; set; }
        public int Id { get; set; }
        public string Nameuser { get; set; }
        public DateTime createdate { get; set; }
        public double? price { get; set; }
        public int? product { get; set; }
        public int Quantity { get; set; }
       // public List<OrderDetail>? OrderDetails { get; set; }


         public ICollection<OrderdetailDTO>details { get; set; }= new List<OrderdetailDTO>();    
    }
    public class OrderdetailDTO 
    {
        public int Quantity { get; set; }
        [Required]
        public int product { get; set; }
    }
}
