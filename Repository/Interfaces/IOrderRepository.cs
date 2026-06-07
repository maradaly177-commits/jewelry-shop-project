using Cosmetic_App.Common.DTO;

namespace Cosmetic_App.Repository.Interfaces
{
    /// <summary>
    /// Repository xử lý đơn hàng (Order)
    /// Người thực hiện: Marada
    /// </summary>
    public interface IOrderRepository
    {
        /// <summary>
        /// Thanh toán đơn hàng (Checkout)
        /// </summary>
        Task<int> Checkout(int userId, string address, string phone);

        /// <summary>
        /// Lấy danh sách đơn hàng theo user (Shopee style preview)
        /// </summary>
        Task<IEnumerable<OrderPreviewDto>> GetOrderPreviewByUser(int userId);

        /// <summary>
        /// Lấy chi tiết đơn hàng theo orderId
        /// </summary>
        Task<IEnumerable<OrderDetailDto>> GetOrderDetail(int orderId);

        /// <summary>
        /// Cập nhật trạng thái đơn hàng
        /// </summary>
        Task UpdateStatus(int orderId, string status);
    }
}