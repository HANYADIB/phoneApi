using phoneApi.Models.Domain;
using phoneApi.Models.DTO;

namespace phoneApi.Mapping
{
    public static class testmapper
    {
        public static ProductssDto ToProductssDto(this Product commentModel)
        {
            return new ProductssDto
            {
               Product_Id = commentModel.Product_Id,
                Price = commentModel.Price,
                Name = commentModel.Name,
                Cat_Id=commentModel.Category_Id,
                Cat_Name = commentModel.Categorys.Namecat,
                Sup_Id = commentModel.Supplier_Id,
                Sub_Name = commentModel.Suppliers.Namesup,
                Image = commentModel.Image,
                Description=commentModel.Description,
                

            };
        }
    }
}
