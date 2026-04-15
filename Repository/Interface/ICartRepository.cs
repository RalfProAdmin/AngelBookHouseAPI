using EkartAPI.Models;
using EkartAPI.Models.ResponseModels;

namespace EkartAPI.Repository.Interface
{
    public interface ICartRepository
    {
        Task AddToCartAsync(Cart cart);
        Task<IEnumerable<CartItemDTO>> GetAllCartItemsAsync(int userId);
        Task<Cart> GetCartItemByIdAsync(int cartId);
        Task UpdateCartQuantity(int userId, int productId, int quantityChange);
        Task<CartFKResponseModel> GetCartItemByIdFKAsync(int userId);
        Task<Cart> GetCartDetailsByUserAndProductId(int userId, int productId);
        Task UpdateCartItemAsync(Cart cart);
        Task DeleteCartItemAsync(int cartId);
        Task<Cart> DeleteCartItemByProductId(int ProductId);
        Task InsertCartItemsAsync(List<CartItem> cartItems);
    }
}
