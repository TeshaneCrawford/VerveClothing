using VerveClothingApi.DTOs;
using VerveClothingApi.Interfaces;

namespace VerveClothingApi.Services
{
    public interface IWishlistItemService
    {
        Task<WishlistItemDto> GetWishlistItemByIdAsync(int id);
        Task<IEnumerable<WishlistItemDto>> GetWishlistItemsByUserIdAsync(int userId);
        Task<WishlistItemDto> AddToWishlistAsync(CreateWishlistItemDto createWishlistItemDto);
        Task<bool> RemoveFromWishlistAsync(int id);
    }

    public class WishlistItemService(IWishlistItemRepository wishlistItemRepository) : IWishlistItemService
    {
        private readonly IWishlistItemRepository _wishlistItemRepository = wishlistItemRepository;

        public async Task<WishlistItemDto> GetWishlistItemByIdAsync(int id)
        {
            return await _wishlistItemRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<WishlistItemDto>> GetWishlistItemsByUserIdAsync(int userId)
        {
            return await _wishlistItemRepository.GetByUserIdAsync(userId);
        }

        public async Task<WishlistItemDto> AddToWishlistAsync(CreateWishlistItemDto createWishlistItemDto)
        {
            // Todo. Add additional business logic here, e.g., checking if the item already exists in the user's wishlist
            var existingItems = await _wishlistItemRepository.GetByUserIdAsync(createWishlistItemDto.UserId);
            if (existingItems.Any(item => item.ProductId == createWishlistItemDto.ProductId))
            {
                // Item already exists in the wishlist
                return null;
            }

            return await _wishlistItemRepository.CreateAsync(createWishlistItemDto);
        }

        public async Task<bool> RemoveFromWishlistAsync(int id)
        {
            return await _wishlistItemRepository.DeleteAsync(id);
        }
    }
}
