using System.ComponentModel.DataAnnotations;

namespace VerveClothingApi.Entities
{
    public class ProductImage
    {
        public int ImageId { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        public int DisplayOrder { get; set; }
    }
}
