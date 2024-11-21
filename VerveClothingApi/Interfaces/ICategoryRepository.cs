using VerveClothingApi.DTOs;

namespace VerveClothingApi.Interfaces
{
    public interface ICategoryRepository
    {
        Task<CategoryDto> GetByIdAsync(int id);
        Task<IEnumerable<CategoryDto>> GetAllAsync();
        Task<CategoryDto> CreateAsync(CreateCategoryDto createCategoryDto);
        Task<CategoryDto> UpdateAsync(int id, UpdateCategoryDto updateCategoryDto);
        Task<bool> DeleteAsync(int id);
    }
}
