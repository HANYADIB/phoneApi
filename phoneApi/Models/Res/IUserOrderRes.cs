using phoneApi.Models.Domain;

namespace phoneApi.Models.Res
{
    public interface IUserOrderRes
    {
        Task<IEnumerable<Order>> UserOrders();
        List<Order> GetAllOrder(string Id);
        int GetNumdersOrder(string Id);
    }
}