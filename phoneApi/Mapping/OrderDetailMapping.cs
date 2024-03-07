using phoneApi.Models.Domain;
using phoneApi.Models.DTO;

namespace phoneApi.Mapping
{
    public static class OrderDetailMapping
    {
        public static OrderDetailDto ToOrderDetailDto(this OrderDetail Model)
        {
            return new OrderDetailDto
            {
                Qty = Model.Quantity,
                UnitPrice = Model.UnitPrice,
              //  pro = Model.Products.Select(p => p.ToProductssDto()).ToString(),
              //Products= Model.Products,
              Name = Model.Products.Name,
              Cat_Name=Model.Products.Categorys.Namecat,
              
                //Productss = Model.Products.Name.ToString(),
                //Productss = Model.Products.Select(c => c.
            };
        }
    }
}
