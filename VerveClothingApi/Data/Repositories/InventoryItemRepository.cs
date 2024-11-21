using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VerveClothingApi.DTOs;
using VerveClothingApi.Entities;
using VerveClothingApi.Interfaces;

namespace VerveClothingApi.Data.Repositories
{
    public class InventoryItemRepository(ApplicationDbContext context, IMapper mapper) : IInventoryItemRepository
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<InventoryItemDto> GetByIdAsync(int id)
        {
            var inventoryItem = await _context.InventoryItems.FindAsync(id);
            return _mapper.Map<InventoryItemDto>(inventoryItem);
        }

        public async Task<InventoryItemDto> GetByVariantIdAsync(int variantId)
        {
            var inventoryItem = await _context.InventoryItems
                .FirstOrDefaultAsync(i => i.VariantId == variantId);
            return _mapper.Map<InventoryItemDto>(inventoryItem);
        }

        public async Task<IEnumerable<InventoryItemDto>> GetAllAsync()
        {
            var inventoryItems = await _context.InventoryItems.ToListAsync();
            return _mapper.Map<IEnumerable<InventoryItemDto>>(inventoryItems);
        }

        public async Task<InventoryItemDto> CreateAsync(CreateInventoryItemDto createInventoryItemDto)
        {
            var inventoryItem = _mapper.Map<InventoryItem>(createInventoryItemDto);
            _context.InventoryItems.Add(inventoryItem);
            await _context.SaveChangesAsync();
            return _mapper.Map<InventoryItemDto>(inventoryItem);
        }

        public async Task<InventoryItemDto> UpdateAsync(int id, UpdateInventoryItemDto updateInventoryItemDto)
        {
            var inventoryItem = await _context.InventoryItems.FindAsync(id);
            if (inventoryItem == null) return null;

            _mapper.Map(updateInventoryItemDto, inventoryItem);
            await _context.SaveChangesAsync();
            return _mapper.Map<InventoryItemDto>(inventoryItem);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var inventoryItem = await _context.InventoryItems.FindAsync(id);
            if (inventoryItem == null) return false;

            _context.InventoryItems.Remove(inventoryItem);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
