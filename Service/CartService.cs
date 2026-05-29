using Cosmetic_App.Common.DTO;
using Cosmetic_App.Repository.Interfaces;
using Cosmetic_App.Service.Interfaces;

namespace Cosmetic_App.Service
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        // =========================
        // ADD TO CART
        // =========================
        public async Task AddToCart(int userId, int productId, int quantity)
        {
            await _cartRepository.AddToCart(userId, productId, quantity);
        }

        // =========================
        // GET CART
        // =========================
        public async Task<IEnumerable<CartItemDto>> GetCart(int userId)
        {
            return await _cartRepository.GetCart(userId);
        }

        // =========================
        // INCREASE
        // =========================
        public async Task Increase(int userId, int productId)
        {
            await _cartRepository.Increase(userId, productId);
        }

        // =========================
        // DECREASE
        // =========================
        public async Task Decrease(int userId, int productId)
        {
            await _cartRepository.Decrease(userId, productId);
        }

        // =========================
        // REMOVE
        // =========================
        public async Task Remove(int userId, int productId)
        {
            await _cartRepository.Remove(userId, productId);
        }
    }
}