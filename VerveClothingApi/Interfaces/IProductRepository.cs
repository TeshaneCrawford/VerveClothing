using VerveClothingApi.DTOs;
using VerveClothingApi.Common;

namespace VerveClothingApi.Interfaces
{
    public interface IProductRepository
    {
        Task<ProductDto> GetByIdAsync(int id);
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<ProductDto> CreateAsync(CreateProductDto createProductDto);
        Task<ProductDto> UpdateAsync(int id, UpdateProductDto updateProductDto);
        Task<bool> DeleteAsync(int id);
        Task<PagedResult<ProductDto>> GetFilteredAsync(ProductFilterParams filterParams);
        Task<IEnumerable<ProductDto>> SearchByTermAsync(string searchTerm);
        Task<int> GetStockQuantityAsync(int productId);
    }
}
