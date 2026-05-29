using Cosmetic_App.Common.DTO;

namespace Cosmetic_App.Repository.Interfaces
{
    public interface ICartRepository
    {
        Task AddToCart(int userId, int productId, int quantity);

        Task<IEnumerable<CartItemDto>> GetCart(int userId);

        Task Increase(int userId, int productId);

        Task Decrease(int userId, int productId);

        Task Remove(int userId, int productId);
    }
}