using System.Text.Json.Serialization;

namespace phoneApi.Models.Domain
{
    public class Supplier
    {
        public int Id { get; set; }
        public string Namesup { get; set; }
       // [JsonIgnore]
        public virtual List<Product>? Products { get; set; }
    }
}
