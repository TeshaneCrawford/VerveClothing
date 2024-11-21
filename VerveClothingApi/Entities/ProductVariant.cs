using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace VerveClothingApi.Entities
{
    public class ProductVariant
    {
        public int VariantId { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int SizeId { get; set; }
        public Size Size { get; set; }
        public int ColorId { get; set; }
        public Color Color { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal PriceAdjustment { get; set; }
        [Required]
        public string SKU { get; set; }

        public ICollection<InventoryItem> InventoryItems { get; set; }
    }
}
