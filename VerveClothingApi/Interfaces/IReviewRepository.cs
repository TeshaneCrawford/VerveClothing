using VerveClothingApi.DTOs;

namespace VerveClothingApi.Interfaces
{
    public interface IReviewRepository
    {
        Task<ReviewDto> GetByIdAsync(int id);
        Task<IEnumerable<ReviewDto>> GetAllAsync();
        Task<IEnumerable<ReviewDto>> GetByProductIdAsync(int productId);
        Task<IEnumerable<ReviewDto>> GetByUserIdAsync(int userId);
        Task<ReviewDto> CreateAsync(CreateReviewDto createReviewDto);
        Task<ReviewDto> UpdateAsync(int id, UpdateReviewDto updateReviewDto);
        Task<bool> DeleteAsync(int id);
    }
}
