using VerveClothingApi.DTOs;
using VerveClothingApi.Interfaces;

namespace VerveClothingApi.Services
{
    public interface IProductVariantService
    {
        Task<ProductVariantDto> GetProductVariantByIdAsync(int id);
        Task<IEnumerable<ProductVariantDto>> GetAllProductVariantsAsync();
        Task<IEnumerable<ProductVariantDto>> GetProductVariantsByProductIdAsync(int productId);
        Task<ProductVariantDto> CreateProductVariantAsync(CreateProductVariantDto createProductVariantDto);
        Task<ProductVariantDto> UpdateProductVariantAsync(int id, UpdateProductVariantDto updateProductVariantDto);
        Task<bool> DeleteProductVariantAsync(int id);
    }

    public class ProductVariantService(IProductVariantRepository productVariantRepository) : IProductVariantService
    {
        private readonly IProductVariantRepository _productVariantRepository = productVariantRepository;

        public async Task<ProductVariantDto> GetProductVariantByIdAsync(int id)
        {
            return await _productVariantRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<ProductVariantDto>> GetAllProductVariantsAsync()
        {
            return await _productVariantRepository.GetAllAsync();
        }

        public async Task<IEnumerable<ProductVariantDto>> GetProductVariantsByProductIdAsync(int productId)
        {
            return await _productVariantRepository.GetByProductIdAsync(productId);
        }

        public async Task<ProductVariantDto> CreateProductVariantAsync(CreateProductVariantDto createProductVariantDto)
        {
            return await _productVariantRepository.CreateAsync(createProductVariantDto);
        }

        public async Task<ProductVariantDto> UpdateProductVariantAsync(int id, UpdateProductVariantDto updateProductVariantDto)
        {
            return await _productVariantRepository.UpdateAsync(id, updateProductVariantDto);
        }

        public async Task<bool> DeleteProductVariantAsync(int id)
        {
            return await _productVariantRepository.DeleteAsync(id);
        }
    }

}
