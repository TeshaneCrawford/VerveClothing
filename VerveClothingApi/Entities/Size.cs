using System.ComponentModel.DataAnnotations;

namespace VerveClothingApi.Entities
{
    public class Size
    {
        public int SizeId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Category { get; set; }

        public ICollection<ProductVariant> ProductVariants { get; set; }
    }
}
