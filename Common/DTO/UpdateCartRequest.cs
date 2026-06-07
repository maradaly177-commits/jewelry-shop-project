namespace Cosmetic_App.Common.DTO
{
    /// <summary>
    /// Chức năng: Đối tượng hứng dữ liệu thay đổi số lượng từ giao diện Giỏ hàng.
    /// Người tạo: Marada (MSSV: 5001467)
    /// </summary>
    public class UpdateCartRequest
    {
        /// Người thực hiện: Vortey
        public string OrderDetailId { get; set; } = string.Empty;
        public int NewQuantity { get; set; }
    }
}