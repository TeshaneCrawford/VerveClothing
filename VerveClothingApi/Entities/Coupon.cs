using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VerveClothingApi.Entities
{
    public class Coupon
    {
        public int CouponId { get; set; }
        [Required]
        public string Code { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? DiscountAmount { get; set; }
        public float? DiscountPercentage { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public int UsageLimit { get; set; }
        public int TimesUsed { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
