using phoneApi.Models.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace phoneApi.Models.DTO
{
    public class proDto
    {
        //[Key]
        //public int Product_Id { get; set; } = 0;
        [Required]
        // [Remote(action: "NameCheck", controller: "product", AdditionalFields = "Product_Id")]
        public string Name { get; set; }

        public double Price { get; set; }

       public string? Image { get; set; }
        [NotMapped]
       public IFormFile? File { get; set; }
        public string? Description { get; set; }
       
        public int Cat_Id { get; set; }
      
        public int Sup_Id { get; set; }
       


    }
}
