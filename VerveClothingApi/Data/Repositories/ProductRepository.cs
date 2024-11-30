using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VerveClothingApi.DTOs;
using VerveClothingApi.Entities;
using VerveClothingApi.Interfaces;
using VerveClothingApi.Common;

namespace VerveClothingApi.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var product = await _context.Products
                .Include(p => p.ProductVariants)
                .Include(p => p.Material)
                .FirstOrDefaultAsync(p => p.ProductId == id);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var products = await _context.Products.ToListAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<ProductDto> CreateAsync(CreateProductDto createProductDto)
        {
            var product = _mapper.Map<Product>(createProductDto);
            product.CreatedAt = DateTime.UtcNow;
            product.UpdatedAt = DateTime.UtcNow;

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> UpdateAsync(int id, UpdateProductDto updateProductDto)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return null;

            _mapper.Map(updateProductDto, product);
            product.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Common.PagedResult<ProductDto>> GetFilteredAsync(ProductFilterParams filterParams)
        {
            var query = _context.Products.AsQueryable();

            if (filterParams.MinPrice.HasValue)
                query = query.Where(p => p.BasePrice >= filterParams.MinPrice.Value);
            
            if (filterParams.MaxPrice.HasValue)
                query = query.Where(p => p.BasePrice <= filterParams.MaxPrice.Value);

            if (filterParams.IsActive.HasValue)
                query = query.Where(p => p.IsActive == filterParams.IsActive.Value);

            var totalItems = await query.CountAsync();
            
            if (!string.IsNullOrEmpty(filterParams.SortBy))
            {
                query = filterParams.SortDesc 
                    ? query.OrderByDescending(p => EF.Property<object>(p, filterParams.SortBy))
                    : query.OrderBy(p => EF.Property<object>(p, filterParams.SortBy));
            }

            var items = await query
                .Skip((filterParams.Page - 1) * filterParams.PageSize)
                .Take(filterParams.PageSize)
                .ToListAsync();

            return new Common.PagedResult<ProductDto>(
                _mapper.Map<IEnumerable<ProductDto>>(items),
                totalItems,
                filterParams.Page,
                filterParams.PageSize
            );
        }

        public async Task<IEnumerable<ProductDto>> SearchByTermAsync(string searchTerm)
        {
            var products = await _context.Products
                .Where(p => p.Name.Contains(searchTerm) || p.Description.Contains(searchTerm))
                .ToListAsync();
            
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<int> GetStockQuantityAsync(int productId)
        {
            var stockQuantity = await _context.ProductVariants
                .Where(pv => pv.ProductId == productId)
                .Select(pv => pv.StockQuantity)
                .SumAsync();

            return stockQuantity;
        }
    }
}
