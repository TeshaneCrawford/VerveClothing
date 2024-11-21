using VerveClothingApi.DTOs;

namespace VerveClothingApi.Interfaces
{
    public interface ICouponRepository
    {
        Task<CouponDto> GetByIdAsync(int id);
        Task<CouponDto> GetByCodeAsync(string code);
        Task<IEnumerable<CouponDto>> GetAllAsync();
        Task<CouponDto> CreateAsync(CreateCouponDto createCouponDto);
        Task<CouponDto> UpdateAsync(int id, UpdateCouponDto updateCouponDto);
        Task<bool> DeleteAsync(int id);
        Task<bool> IncrementUsageAsync(int id);
    }
}
