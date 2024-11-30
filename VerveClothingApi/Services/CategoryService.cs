using VerveClothingApi.DTOs;
using VerveClothingApi.Interfaces;
using VerveClothingApi.Exceptions;

namespace VerveClothingApi.Services
{
    public interface ICategoryService
    {
        Task<CategoryDto> GetCategoryByIdAsync(int id);
        Task<PagedResult<CategoryDto>> GetAllCategoriesAsync(int page = 1, int pageSize = 10);
        Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto createCategoryDto);
        Task<CategoryDto> UpdateCategoryAsync(int id, UpdateCategoryDto updateCategoryDto);
        Task<bool> DeleteCategoryAsync(int id);
    }

    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryDto> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
                throw new NotFoundException($"Category with id {id} not found");
            return category;
        }

        public async Task<PagedResult<CategoryDto>> GetAllCategoriesAsync(int page = 1, int pageSize = 10)
        {
            return await _categoryRepository.GetAllAsync(page, pageSize);
        }

        public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            return await _categoryRepository.CreateAsync(createCategoryDto);
        }

        public async Task<CategoryDto> UpdateCategoryAsync(int id, UpdateCategoryDto updateCategoryDto)
        {
            if (updateCategoryDto.ParentCategoryId.HasValue)
            {
                var parentExists = await _categoryRepository.ExistsAsync(updateCategoryDto.ParentCategoryId.Value);
                if (!parentExists)
                    throw new NotFoundException($"Parent category with id {updateCategoryDto.ParentCategoryId.Value} not found");
            }
            
            var result = await _categoryRepository.UpdateAsync(id, updateCategoryDto);
            if (result == null)
                throw new NotFoundException($"Category with id {id} not found");
            return result;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            return await _categoryRepository.DeleteAsync(id);
        }
    }
}
