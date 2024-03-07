using phoneApi.Models.Domain;
using phoneApi.Models.DTO;

namespace phoneApi.Mapping
{
    public static class ProductMapper
    {
        public static ProductsDto ToProductsDto(this Product commentModel)
        {
            return new ProductsDto
            {
                Product_Id = commentModel.Product_Id,
                Price = commentModel.Price,
                Description = commentModel.Description,
                Name = commentModel.Name,
                Cat_Name = commentModel.Categorys.Namecat,
                Cat_Id = commentModel.Category_Id,
                
            };
        }
    }
}
