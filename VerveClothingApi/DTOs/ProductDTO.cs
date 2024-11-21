using System.ComponentModel.DataAnnotations;

namespace VerveClothingApi.DTOs
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal BasePrice { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int MaterialId { get; set; }
    }

    public class CreateProductDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public decimal BasePrice { get; set; }
        public bool IsActive { get; set; }
        [Required]
        public int MaterialId { get; set; }
    }

    public class UpdateProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? BasePrice { get; set; }
        public bool? IsActive { get; set; }
        public int? MaterialId { get; set; }
    }
}
