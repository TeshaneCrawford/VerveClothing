using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VerveClothingApi.DTOs;
using VerveClothingApi.Services;

namespace VerveClothingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly ICouponService _couponService;
        private readonly ILogger<CouponController> _logger;

        public CouponController(ICouponService couponService, ILogger<CouponController> logger)
        {
            _couponService = couponService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CouponDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCoupons()
        {
            var coupons = await _couponService.GetAllCouponsAsync();
            return Ok(coupons);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CouponDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCoupon(int id)
        {
            var coupon = await _couponService.GetCouponByIdAsync(id);
            if (coupon == null) return NotFound();
            return Ok(coupon);
        }

        [HttpGet("code/{code}")]
        [ProducesResponseType(typeof(CouponDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCouponByCode(string code)
        {
            var coupon = await _couponService.GetCouponByCodeAsync(code);
            if (coupon == null) return NotFound();
            return Ok(coupon);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CouponDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCoupon([FromBody] CreateCouponDto createCouponDto)
        {
            try
            {
                var coupon = await _couponService.CreateCouponAsync(createCouponDto);
                return CreatedAtAction(nameof(GetCoupon), new { id = coupon.CouponId }, coupon);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating coupon");
                return BadRequest(new { message = "Error creating coupon" });
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CouponDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCoupon(int id, [FromBody] UpdateCouponDto updateCouponDto)
        {
            try
            {
                var coupon = await _couponService.UpdateCouponAsync(id, updateCouponDto);
                if (coupon == null) return NotFound();
                return Ok(coupon);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating coupon");
                return BadRequest(new { message = "Error updating coupon" });
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCoupon(int id)
        {
            var result = await _couponService.DeleteCouponAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpPost("{id}/apply")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ApplyCoupon(int id, [FromQuery] decimal orderTotal)
        {
            var result = await _couponService.ApplyCouponAsync(id, orderTotal);
            if (!result) return BadRequest(new { message = "Coupon cannot be applied" });
            return Ok(new { message = "Coupon applied successfully" });
        }
    }
}
