using System.ComponentModel.DataAnnotations;

namespace VerveClothingApi.Entities
{
    public class Color
    {
        public int ColorId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string HexCode { get; set; }

        public ICollection<ProductVariant> ProductVariants { get; set; }
    }
}
