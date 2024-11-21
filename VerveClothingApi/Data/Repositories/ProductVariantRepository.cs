using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VerveClothingApi.DTOs;
using VerveClothingApi.Entities;
using VerveClothingApi.Interfaces;

namespace VerveClothingApi.Data.Repositories
{
    public class ProductVariantRepository(ApplicationDbContext context, IMapper mapper) : IProductVariantRepository
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<ProductVariantDto> GetByIdAsync(int id)
        {
            var variant = await _context.ProductVariants.FindAsync(id);
            return _mapper.Map<ProductVariantDto>(variant);
        }

        public async Task<IEnumerable<ProductVariantDto>> GetAllAsync()
        {
            var variants = await _context.ProductVariants.ToListAsync();
            return _mapper.Map<IEnumerable<ProductVariantDto>>(variants);
        }

        public async Task<IEnumerable<ProductVariantDto>> GetByProductIdAsync(int productId)
        {
            var variants = await _context.ProductVariants
                .Where(v => v.ProductId == productId)
                .ToListAsync();
            return _mapper.Map<IEnumerable<ProductVariantDto>>(variants);
        }

        public async Task<ProductVariantDto> CreateAsync(CreateProductVariantDto createProductVariantDto)
        {
            var variant = _mapper.Map<ProductVariant>(createProductVariantDto);
            _context.ProductVariants.Add(variant);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductVariantDto>(variant);
        }

        public async Task<ProductVariantDto> UpdateAsync(int id, UpdateProductVariantDto updateProductVariantDto)
        {
            var variant = await _context.ProductVariants.FindAsync(id);
            if (variant == null) return null;

            _mapper.Map(updateProductVariantDto, variant);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductVariantDto>(variant);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var variant = await _context.ProductVariants.FindAsync(id);
            if (variant == null) return false;

            _context.ProductVariants.Remove(variant);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
