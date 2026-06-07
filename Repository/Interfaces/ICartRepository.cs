using Cosmetic_App.Common.DTO;

namespace Cosmetic_App.Repository.Interfaces
{
    /// <summary>
    /// Interface xử lý giỏ hàng
    /// Người thực hiện: Marada
    /// </summary>
    public interface ICartRepository
    {
        /// <summary>
        /// Thêm sản phẩm vào giỏ hàng
        /// </summary>
        Task AddToCart(int userId, int productId, int quantity);

        /// <summary>
        /// Lấy danh sách giỏ hàng theo user
        /// </summary>
        Task<IEnumerable<CartItemDto>> GetCart(int userId);

        /// <summary>
        /// Tăng số lượng sản phẩm trong giỏ
        /// </summary>
        Task Increase(int userId, int productId);

        /// <summary>
        /// Giảm số lượng sản phẩm trong giỏ
        /// </summary>
        Task Decrease(int userId, int productId);

        /// <summary>
        /// Xóa sản phẩm khỏi giỏ
        /// </summary>
        Task Remove(int userId, int productId);
    }
}