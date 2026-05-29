using Cosmetic_App.Common.DTO;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repo;

    public OrderService(IOrderRepository repo)
    {
        _repo = repo;
    }

    public Task<int> Checkout(int userId, string address, string phone)
        => _repo.Checkout(userId, address, phone);

    public Task<IEnumerable<OrderDto>> GetOrdersByUser(int userId)
        => _repo.GetOrdersByUser(userId);

    public Task<IEnumerable<OrderDetailDto>> GetOrderDetail(int orderId)
        => _repo.GetOrderDetail(orderId);
}