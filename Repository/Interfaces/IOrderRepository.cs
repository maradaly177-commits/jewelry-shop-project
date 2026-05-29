using Cosmetic_App.Common.DTO;

public interface IOrderRepository
{
    Task<int> Checkout(int userId, string address, string phone);

    Task<IEnumerable<OrderDto>> GetOrdersByUser(int userId);

    Task<IEnumerable<OrderDetailDto>> GetOrderDetail(int orderId);
}