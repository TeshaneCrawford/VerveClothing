using System.ComponentModel.DataAnnotations;

namespace VerveClothingApi.DTOs
{
    public class CouponDto
    {
        public int CouponId { get; set; }
        public string Code { get; set; }
        public decimal? DiscountAmount { get; set; }
        public float? DiscountPercentage { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public int UsageLimit { get; set; }
        public int TimesUsed { get; set; }
        public bool IsActive { get; set; }
        public decimal? MinimumPurchaseAmount { get; set; }
    }

    public class CreateCouponDto
    {
        [Required]
        public string Code { get; set; }
        public decimal? DiscountAmount { get; set; }
        public float? DiscountPercentage { get; set; }
        [Required]
        public DateTime ValidFrom { get; set; }
        [Required]
        public DateTime ValidTo { get; set; }
        [Required]
        public int UsageLimit { get; set; }
        public bool IsActive { get; set; }
        public decimal? MinimumPurchaseAmount { get; set; }
    }

    public class UpdateCouponDto
    {
        public string Code { get; set; }
        public decimal? DiscountAmount { get; set; }
        public float? DiscountPercentage { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public int? UsageLimit { get; set; }
        public bool? IsActive { get; set; }
        public decimal? MinimumPurchaseAmount { get; set; }
    }
}
