using phoneApi.Models.Domain;
using phoneApi.Models.DTO;

namespace phoneApi.Mapping
{
    public static class OrderMapping
    {
        public static OrderDto ToOrderDto(this Order Model)
        {
            return new OrderDto
            {
                Id=Model.Id,
                UserId = Model.UserId,
                NameUser = Model.NameUser,
                IsDeleted = Model.IsDeleted,
                CreateDate = Model.CreateDate,
                OrderDetails = Model.OrderDetails.Select(c => c.ToOrderDetailDto()).ToList(),
                

            };
        }
    }
}
