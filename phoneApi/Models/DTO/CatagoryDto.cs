using phoneApi.Models.Domain;
using System.Collections.Generic;

namespace phoneApi.Models.DTO
{
    public class CatagoryDto
    {
        public int Cat_Id { get; set; }
        public string  Namecat { get; set; }
        public byte[]? Img { get; set; }

        // public List<string> NameProduct { get; set; }
        public List<ProductsDto> Products { get; set; }
        //public List<int> id { get; set; }=new List<int>();
    }
}
