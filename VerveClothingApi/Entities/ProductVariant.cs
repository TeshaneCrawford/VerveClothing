using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace VerveClothingApi.Entities
{
    public class ProductVariant
    {
        public int ProductVariantId { get; set; }
        public int ProductId { get; set; }
        public int SizeId { get; set; }
        public int ColorId { get; set; }
        public int StockQuantity { get; set; }
        public decimal PriceModifier { get; set; }
        public string SKU { get; set; }
        
        public Product Product { get; set; }
        public Size Size { get; set; }
        public Color Color { get; set; }
    }
}
