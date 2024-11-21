using VerveClothingApi.DTOs;

namespace VerveClothingApi.Interfaces
{
    public interface IProductRepository
    {
        Task<ProductDto> GetByIdAsync(int id);
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<ProductDto> CreateAsync(CreateProductDto createProductDto);
        Task<ProductDto> UpdateAsync(int id, UpdateProductDto updateProductDto);
        Task<bool> DeleteAsync(int id);
    }
}
