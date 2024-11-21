using VerveClothingApi.DTOs;
using VerveClothingApi.Interfaces;

namespace VerveClothingApi.Services
{
    public interface IReviewService
    {
        Task<ReviewDto> GetReviewByIdAsync(int id);
        Task<IEnumerable<ReviewDto>> GetAllReviewsAsync();
        Task<IEnumerable<ReviewDto>> GetReviewsByProductIdAsync(int productId);
        Task<IEnumerable<ReviewDto>> GetReviewsByUserIdAsync(int userId);
        Task<ReviewDto> CreateReviewAsync(CreateReviewDto createReviewDto);
        Task<ReviewDto> UpdateReviewAsync(int id, UpdateReviewDto updateReviewDto);
        Task<bool> DeleteReviewAsync(int id);
    }

    public class ReviewService(IReviewRepository reviewRepository) : IReviewService
    {
        private readonly IReviewRepository _reviewRepository = reviewRepository;

        public async Task<ReviewDto> GetReviewByIdAsync(int id)
        {
            return await _reviewRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<ReviewDto>> GetAllReviewsAsync()
        {
            return await _reviewRepository.GetAllAsync();
        }

        public async Task<IEnumerable<ReviewDto>> GetReviewsByProductIdAsync(int productId)
        {
            return await _reviewRepository.GetByProductIdAsync(productId);
        }

        public async Task<IEnumerable<ReviewDto>> GetReviewsByUserIdAsync(int userId)
        {
            return await _reviewRepository.GetByUserIdAsync(userId);
        }

        public async Task<ReviewDto> CreateReviewAsync(CreateReviewDto createReviewDto)
        {
            // Todo. Add additional business logic here, e.g., checking if the user has purchased the product
            return await _reviewRepository.CreateAsync(createReviewDto);
        }

        public async Task<ReviewDto> UpdateReviewAsync(int id, UpdateReviewDto updateReviewDto)
        {
            // Todo. Add additional business logic here, e.g., checking if the user is the owner of the review
            return await _reviewRepository.UpdateAsync(id, updateReviewDto);
        }

        public async Task<bool> DeleteReviewAsync(int id)
        {
            // Todo. Add additional business logic here, e.g., checking if the user is the owner of the review or an admin
            return await _reviewRepository.DeleteAsync(id);
        }
    }
}
