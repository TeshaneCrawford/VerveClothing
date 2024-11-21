using System.ComponentModel.DataAnnotations;

namespace VerveClothingApi.DTOs
{
    public class ProductVariantDto
    {
        public int VariantId { get; set; }
        public int ProductId { get; set; }
        public int SizeId { get; set; }
        public int ColorId { get; set; }
        public decimal PriceAdjustment { get; set; }
        public string SKU { get; set; }
    }

    public class CreateProductVariantDto
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int SizeId { get; set; }
        [Required]
        public int ColorId { get; set; }
        [Required]
        public decimal PriceAdjustment { get; set; }
        [Required]
        public string SKU { get; set; }
    }

    public class UpdateProductVariantDto
    {
        public int? SizeId { get; set; }
        public int? ColorId { get; set; }
        public decimal? PriceAdjustment { get; set; }
        public string SKU { get; set; }
    }
}
