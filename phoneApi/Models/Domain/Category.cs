using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace phoneApi.Models.Domain
{
    public class Category
    {

        [Key]
        public int Cat_Id { get; set; }
        public string Namecat { get; set; }
        public byte[]? Img { get; set; }
        //[JsonIgnore]
        public virtual List<Product> Products { get; set; } 
    }
}
