using VerveClothingApi.DTOs;

namespace VerveClothingApi.Interfaces
{
    public interface IProductVariantRepository
    {
        Task<ProductVariantDto> GetByIdAsync(int id);
        Task<IEnumerable<ProductVariantDto>> GetAllAsync();
        Task<IEnumerable<ProductVariantDto>> GetByProductIdAsync(int productId);
        Task<ProductVariantDto> CreateAsync(CreateProductVariantDto createProductVariantDto);
        Task<ProductVariantDto> UpdateAsync(int id, UpdateProductVariantDto updateProductVariantDto);
        Task<bool> DeleteAsync(int id);
    }
}
