using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VerveClothingApi.DTOs;
using VerveClothingApi.Entities;
using VerveClothingApi.Interfaces;

namespace VerveClothingApi.Data.Repositories
{
    public class WishlistItemRepository(ApplicationDbContext context, IMapper mapper) : IWishlistItemRepository
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<WishlistItemDto> GetByIdAsync(int id)
        {
            var wishlistItem = await _context.WishlistItems.FindAsync(id);
            return _mapper.Map<WishlistItemDto>(wishlistItem);
        }

        public async Task<IEnumerable<WishlistItemDto>> GetByUserIdAsync(int userId)
        {
            var wishlistItems = await _context.WishlistItems
                .Where(w => w.UserId == userId)
                .ToListAsync();
            return _mapper.Map<IEnumerable<WishlistItemDto>>(wishlistItems);
        }

        public async Task<WishlistItemDto> CreateAsync(CreateWishlistItemDto createWishlistItemDto)
        {
            var wishlistItem = _mapper.Map<WishlistItem>(createWishlistItemDto);
            _context.WishlistItems.Add(wishlistItem);
            await _context.SaveChangesAsync();
            return _mapper.Map<WishlistItemDto>(wishlistItem);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var wishlistItem = await _context.WishlistItems.FindAsync(id);
            if (wishlistItem == null) return false;

            _context.WishlistItems.Remove(wishlistItem);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
