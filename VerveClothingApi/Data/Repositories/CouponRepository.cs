using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VerveClothingApi.DTOs;
using VerveClothingApi.Entities;
using VerveClothingApi.Interfaces;

namespace VerveClothingApi.Data.Repositories
{
    public class CouponRepository(ApplicationDbContext context, IMapper mapper) : ICouponRepository
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<CouponDto> GetByIdAsync(int id)
        {
            var coupon = await _context.Coupons.FindAsync(id);
            return _mapper.Map<CouponDto>(coupon);
        }

        public async Task<CouponDto> GetByCodeAsync(string code)
        {
            var coupon = await _context.Coupons
                .FirstOrDefaultAsync(c => c.Code == code);
            return _mapper.Map<CouponDto>(coupon);
        }

        public async Task<IEnumerable<CouponDto>> GetAllAsync()
        {
            var coupons = await _context.Coupons.ToListAsync();
            return _mapper.Map<IEnumerable<CouponDto>>(coupons);
        }

        public async Task<CouponDto> CreateAsync(CreateCouponDto createCouponDto)
        {
            var coupon = _mapper.Map<Coupon>(createCouponDto);
            _context.Coupons.Add(coupon);
            await _context.SaveChangesAsync();
            return _mapper.Map<CouponDto>(coupon);
        }

        public async Task<CouponDto> UpdateAsync(int id, UpdateCouponDto updateCouponDto)
        {
            var coupon = await _context.Coupons.FindAsync(id);
            if (coupon == null) return null;

            _mapper.Map(updateCouponDto, coupon);
            await _context.SaveChangesAsync();
            return _mapper.Map<CouponDto>(coupon);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var coupon = await _context.Coupons.FindAsync(id);
            if (coupon == null) return false;

            _context.Coupons.Remove(coupon);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> IncrementUsageAsync(int id)
        {
            var coupon = await _context.Coupons.FindAsync(id);
            if (coupon == null) return false;

            coupon.TimesUsed++;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
