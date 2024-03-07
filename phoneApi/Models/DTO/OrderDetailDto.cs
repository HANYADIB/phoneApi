using phoneApi.Models.Domain;

namespace phoneApi.Models.DTO
{
    public class OrderDetailDto
    {
        public int Qty { get; set; }
        public double UnitPrice { get; set; }
        //public List<ProductssDto> pro { get; set; }
        public Product Products { get; set; }
        public string Name { get; set; }
        public string Cat_Name { get; set; }

        // public Product Productss { get; set; }

    }
}
