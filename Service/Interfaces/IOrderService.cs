using Cosmetic_App.Common.DTO;

public interface IOrderService
{
    /// <summary>
    /// Định nghĩa các phương thức nghiệp vụ cho quản lý đơn hàng.
    /// Người thực hiện: Marada
    /// </summary>
    Task<int> Checkout(
        int userId,
        string address,
        string phone
    );

    // Danh sách đơn hàng kiểu Shopee
    Task<IEnumerable<OrderPreviewDto>>
        GetOrderPreviewByUser(int userId);

    // Chi tiết đơn hàng
    Task<IEnumerable<OrderDetailDto>>
        GetOrderDetail(int orderId);

    // Cập nhật trạng thái
    Task UpdateStatus(
        int orderId,
        string status
    );
}