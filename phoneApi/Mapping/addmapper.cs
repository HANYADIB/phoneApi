using phoneApi.Models.Domain;
using phoneApi.Models.DTO;

namespace phoneApi.Mapping
{
    public static class addmapper
    {
        public static proDto ToproDto(this Product commentModel)
        {
            return new proDto
            {
               // Product_Id = commentModel.Product_Id,
                Price = commentModel.Price,
                Name = commentModel.Name,
                //File = commentModel.File,
                Description = commentModel.Description,
               // Category_Id = commentModel.Category_Id,
                //Supplier_Id = commentModel.Supplier_Id,
             
            };
        }
    }
}
