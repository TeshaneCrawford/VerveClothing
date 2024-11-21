using VerveClothingApi.DTOs;
using VerveClothingApi.Interfaces;

namespace VerveClothingApi.Services
{
    public interface IInventoryItemService
    {
        Task<InventoryItemDto> GetInventoryItemByIdAsync(int id);
        Task<InventoryItemDto> GetInventoryItemByVariantIdAsync(int variantId);
        Task<IEnumerable<InventoryItemDto>> GetAllInventoryItemsAsync();
        Task<InventoryItemDto> CreateInventoryItemAsync(CreateInventoryItemDto createInventoryItemDto);
        Task<InventoryItemDto> UpdateInventoryItemAsync(int id, UpdateInventoryItemDto updateInventoryItemDto);
        Task<bool> DeleteInventoryItemAsync(int id);
        Task<bool> AdjustStockAsync(int id, int quantity);
    }

    public class InventoryItemService(IInventoryItemRepository inventoryItemRepository) : IInventoryItemService
    {
        private readonly IInventoryItemRepository _inventoryItemRepository = inventoryItemRepository;

        public async Task<InventoryItemDto> GetInventoryItemByIdAsync(int id)
        {
            return await _inventoryItemRepository.GetByIdAsync(id);
        }

        public async Task<InventoryItemDto> GetInventoryItemByVariantIdAsync(int variantId)
        {
            return await _inventoryItemRepository.GetByVariantIdAsync(variantId);
        }

        public async Task<IEnumerable<InventoryItemDto>> GetAllInventoryItemsAsync()
        {
            return await _inventoryItemRepository.GetAllAsync();
        }

        public async Task<InventoryItemDto> CreateInventoryItemAsync(CreateInventoryItemDto createInventoryItemDto)
        {
            return await _inventoryItemRepository.CreateAsync(createInventoryItemDto);
        }

        public async Task<InventoryItemDto> UpdateInventoryItemAsync(int id, UpdateInventoryItemDto updateInventoryItemDto)
        {
            return await _inventoryItemRepository.UpdateAsync(id, updateInventoryItemDto);
        }

        public async Task<bool> DeleteInventoryItemAsync(int id)
        {
            return await _inventoryItemRepository.DeleteAsync(id);
        }

        public async Task<bool> AdjustStockAsync(int id, int quantity)
        {
            var inventoryItem = await _inventoryItemRepository.GetByIdAsync(id);
            if (inventoryItem == null)
            {
                return false;
            }

            var newQuantity = inventoryItem.Quantity + quantity;
            if (newQuantity < 0)
            {
                return false;
            }

            var updateDto = new UpdateInventoryItemDto { Quantity = newQuantity };
            var updatedItem = await _inventoryItemRepository.UpdateAsync(id, updateDto);
            return updatedItem != null;
        }
    }
}
