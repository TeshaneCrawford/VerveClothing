using VerveClothingApi.DTOs;
using VerveClothingApi.Exceptions;
using VerveClothingApi.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using VerveClothingApi.Common;
using FluentValidation;

namespace VerveClothingApi.Services
{
    public interface IProductService
    {
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<ProductDto> CreateProductAsync(CreateProductDto createProductDto);
        Task<ProductDto> UpdateProductAsync(int id, UpdateProductDto updateProductDto);
        Task<bool> DeleteProductAsync(int id);
        Task<Common.PagedResult<ProductDto>> GetProductsAsync(ProductFilterParams filterParams);
        Task<IEnumerable<ProductDto>> SearchProductsAsync(string searchTerm);
        Task<bool> ValidateProductAvailabilityAsync(int id, int quantity);
    }

    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMemoryCache _cache;
        private readonly ILogger<ProductService> _logger;
        private readonly IValidator<CreateProductDto> _createValidator;
        private readonly IValidator<UpdateProductDto> _updateValidator;

        public ProductService(
            IProductRepository productRepository,
            IMemoryCache cache,
            ILogger<ProductService> logger,
            IValidator<CreateProductDto> createValidator,
            IValidator<UpdateProductDto> updateValidator)
        {
            _productRepository = productRepository;
            _cache = cache;
            _logger = logger;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var cacheKey = $"product_{id}";
            if (!_cache.TryGetValue(cacheKey, out ProductDto product))
            {
                product = await _productRepository.GetByIdAsync(id);
                if (product == null)
                    throw new ProductNotFoundException(id);

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(10));
                _cache.Set(cacheKey, product, cacheOptions);
            }
            return product;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<Common.PagedResult<ProductDto>> GetProductsAsync(ProductFilterParams filterParams)
        {
            return await _productRepository.GetFilteredAsync(filterParams);
        }

        public async Task<ProductDto> CreateProductAsync(CreateProductDto createProductDto)
        {
            var validationResult = await _createValidator.ValidateAsync(createProductDto);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
            
            try
            {
                var product = await _productRepository.CreateAsync(createProductDto);
                _logger.LogInformation("Product created successfully: {ProductId}", product.ProductId);
                return product;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating product");
                throw;
            }
        }

        public async Task<ProductDto> UpdateProductAsync(int id, UpdateProductDto updateProductDto)
        {
            var validationResult = await _updateValidator.ValidateAsync(updateProductDto);
            if (!validationResult.IsValid)
                throw new FluentValidation.ValidationException(validationResult.Errors);

            var product = await _productRepository.UpdateAsync(id, updateProductDto);
            if (product == null)
                throw new ProductNotFoundException(id);
            
            return product;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            return await _productRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ProductDto>> SearchProductsAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                throw new ArgumentException("Search term cannot be empty");

            return await _productRepository.SearchByTermAsync(searchTerm);
        }

        public async Task<bool> ValidateProductAvailabilityAsync(int id, int quantity)
        {
            var availableQuantity = await _productRepository.GetStockQuantityAsync(id);
            if (availableQuantity < quantity)
                throw new InsufficientStockException(id, quantity, availableQuantity);
            
            return true;
        }
    }
}
