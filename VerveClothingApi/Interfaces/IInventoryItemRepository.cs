using VerveClothingApi.DTOs;

namespace VerveClothingApi.Interfaces
{
    public interface IInventoryItemRepository
    {
        Task<InventoryItemDto> GetByIdAsync(int id);
        Task<InventoryItemDto> GetByVariantIdAsync(int variantId);
        Task<IEnumerable<InventoryItemDto>> GetAllAsync();
        Task<InventoryItemDto> CreateAsync(CreateInventoryItemDto createInventoryItemDto);
        Task<InventoryItemDto> UpdateAsync(int id, UpdateInventoryItemDto updateInventoryItemDto);
        Task<bool> DeleteAsync(int id);
    }
}
