using EkartAPI.Models;
using EkartAPI.Models.ResponseModels;

namespace EkartAPI.Repository.Interface
{
    public interface IWishListRepository
    {
        Task<string> AddToWishListAsync(int userId, int productId);
        Task<ProductFKResponseModel> GetAllWishlistItemsFKAsync(int userId);
        Task<bool> DeleteWishItem(int productId);

        Task<IEnumerable<WishItemDTO>> GetAllWishlistItemsAsync(int userId);
        Task<WishList> GetWishlistItemByIdAsync(int WishId);
        Task<WishList> GetWishlistDetailsByUserAndProductId(int userId, int productId);
        Task<string> GetProductExistOrNotInWishList(int userId, int ProductId);
    }
}
