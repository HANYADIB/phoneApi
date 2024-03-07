using phoneApi.Models.Domain;
using phoneApi.Models.DTO;

namespace phoneApi.Mapping
{
    public static class CategoryMapping
    {
        public static CatagoryDto ToCatagoryDto(this Category stockModel)
        {
            return new CatagoryDto
            {
                Cat_Id = stockModel.Cat_Id,
                Namecat = stockModel.Namecat,
                Img = stockModel.Img,
                Products = stockModel.Products.Select(c => c.ToProductsDto()).ToList()
            };
        }
    }
}