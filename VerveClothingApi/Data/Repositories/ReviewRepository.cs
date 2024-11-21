using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VerveClothingApi.DTOs;
using VerveClothingApi.Entities;
using VerveClothingApi.Interfaces;

namespace VerveClothingApi.Data.Repositories
{
    public class ReviewRepository(ApplicationDbContext context, IMapper mapper) : IReviewRepository
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<ReviewDto> GetByIdAsync(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            return _mapper.Map<ReviewDto>(review);
        }

        public async Task<IEnumerable<ReviewDto>> GetAllAsync()
        {
            var reviews = await _context.Reviews.ToListAsync();
            return _mapper.Map<IEnumerable<ReviewDto>>(reviews);
        }

        public async Task<IEnumerable<ReviewDto>> GetByProductIdAsync(int productId)
        {
            var reviews = await _context.Reviews
                .Where(r => r.ProductId == productId)
                .ToListAsync();
            return _mapper.Map<IEnumerable<ReviewDto>>(reviews);
        }

        public async Task<IEnumerable<ReviewDto>> GetByUserIdAsync(int userId)
        {
            var reviews = await _context.Reviews
                .Where(r => r.UserId == userId)
                .ToListAsync();
            return _mapper.Map<IEnumerable<ReviewDto>>(reviews);
        }

        public async Task<ReviewDto> CreateAsync(CreateReviewDto createReviewDto)
        {
            var review = _mapper.Map<Review>(createReviewDto);
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
            return _mapper.Map<ReviewDto>(review);
        }

        public async Task<ReviewDto> UpdateAsync(int id, UpdateReviewDto updateReviewDto)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null) return null;

            _mapper.Map(updateReviewDto, review);
            await _context.SaveChangesAsync();
            return _mapper.Map<ReviewDto>(review);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null) return false;

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
