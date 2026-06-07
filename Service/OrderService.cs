using Cosmetic_App.Common.DTO;
using Cosmetic_App.Repository.Interfaces;
using Cosmetic_App.Service.Interfaces;

namespace Cosmetic_App.Service
{
    /// <summary>
    /// Lớp triển khai các logic nghiệp vụ liên quan đến đơn hàng.
    /// Điều phối dữ liệu giữa tầng Repository và Controller.
    /// Người thực hiện: Marada
    /// </summary>
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repo;

        public OrderService(IOrderRepository repo)
        {
            _repo = repo;
        }

        public Task<int> Checkout(int userId, string address, string phone)
        {
            return _repo.Checkout(userId, address, phone);
        }

        public Task<IEnumerable<OrderPreviewDto>> GetOrderPreviewByUser(int userId)
        {
            return _repo.GetOrderPreviewByUser(userId);
        }

        public Task<IEnumerable<OrderDetailDto>> GetOrderDetail(int orderId)
        {
            return _repo.GetOrderDetail(orderId);
        }

        public Task UpdateStatus(int orderId, string status)
        {
            return _repo.UpdateStatus(orderId, status);
        }
    }
}