using VerveClothingApi.DTOs;

namespace VerveClothingApi.Interfaces
{
    public interface IWishlistItemRepository
    {
        Task<WishlistItemDto> GetByIdAsync(int id);
        Task<IEnumerable<WishlistItemDto>> GetByUserIdAsync(int userId);
        Task<WishlistItemDto> CreateAsync(CreateWishlistItemDto createWishlistItemDto);
        Task<bool> DeleteAsync(int id);
    }
}
