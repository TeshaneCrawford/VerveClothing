using VerveClothingApi.DTOs;
using VerveClothingApi.Interfaces;

namespace VerveClothingApi.Services
{
    public interface ICouponService
    {
        Task<CouponDto> GetCouponByIdAsync(int id);
        Task<CouponDto> GetCouponByCodeAsync(string code);
        Task<IEnumerable<CouponDto>> GetAllCouponsAsync();
        Task<CouponDto> CreateCouponAsync(CreateCouponDto createCouponDto);
        Task<CouponDto> UpdateCouponAsync(int id, UpdateCouponDto updateCouponDto);
        Task<bool> DeleteCouponAsync(int id);
        Task<bool> ApplyCouponAsync(int id, decimal orderTotal);
    }

    public class CouponService(ICouponRepository couponRepository) : ICouponService
    {
        private readonly ICouponRepository _couponRepository = couponRepository;

        public async Task<CouponDto> GetCouponByIdAsync(int id)
        {
            return await _couponRepository.GetByIdAsync(id);
        }

        public async Task<CouponDto> GetCouponByCodeAsync(string code)
        {
            return await _couponRepository.GetByCodeAsync(code);
        }

        public async Task<IEnumerable<CouponDto>> GetAllCouponsAsync()
        {
            return await _couponRepository.GetAllAsync();
        }

        public async Task<CouponDto> CreateCouponAsync(CreateCouponDto createCouponDto)
        {
            // Todo, Add additional business logic here, e.g., validating the coupon code uniqueness
            return await _couponRepository.CreateAsync(createCouponDto);
        }

        public async Task<CouponDto> UpdateCouponAsync(int id, UpdateCouponDto updateCouponDto)
        {
            return await _couponRepository.UpdateAsync(id, updateCouponDto);
        }

        public async Task<bool> DeleteCouponAsync(int id)
        {
            return await _couponRepository.DeleteAsync(id);
        }

        public async Task<bool> ApplyCouponAsync(int id, decimal orderTotal)
        {
            var coupon = await _couponRepository.GetByIdAsync(id);
            if (coupon == null || !IsCouponValid(coupon, orderTotal))
            {
                return false;
            }
            return await _couponRepository.IncrementUsageAsync(id);
        }

        private static bool IsCouponValid(CouponDto coupon, decimal orderTotal)
        {
            var now = DateTime.UtcNow;
            return coupon.IsActive &&
                   now >= coupon.ValidFrom &&
                   now <= coupon.ValidTo &&
                   coupon.TimesUsed < coupon.UsageLimit &&
                   (coupon.MinimumPurchaseAmount == null || orderTotal >= coupon.MinimumPurchaseAmount);
        }
    }
}
