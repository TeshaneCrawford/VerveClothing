using VerveClothingApi.DTOs;

namespace VerveClothingApi.Interfaces
{
    public interface ICategoryRepository
    {
        Task<CategoryDto> GetByIdAsync(int id);
        Task<PagedResult<CategoryDto>> GetAllAsync(int page = 1, int pageSize = 10);
        Task<bool> ExistsAsync(int id);
        Task<CategoryDto> CreateAsync(CreateCategoryDto createCategoryDto);
        Task<CategoryDto> UpdateAsync(int id, UpdateCategoryDto updateCategoryDto);
        Task<bool> DeleteAsync(int id);
    }
}
