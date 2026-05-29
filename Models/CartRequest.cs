namespace Cosmetic_App.Models
{
    /// <summary>
    /// Chức năng: Đối tượng hứng dữ liệu yêu cầu thêm sản phẩm vào giỏ từ Frontend.
    /// Người tạo: Marada (MSSV: 5001467)
    /// Ngày tạo: 2026-05-22
    /// </summary>
    public class CartRequest
    {
        public int UserId { get; set; } // Sửa sang int để khớp với Entity User và dữ liệu đăng nhập
        public int ProductId { get; set; } // Sửa sang int để khớp với cột Id của bảng products
        public int Quantity { get; set; }
    }
}