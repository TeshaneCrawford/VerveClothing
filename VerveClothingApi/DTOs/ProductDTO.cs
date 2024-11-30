using System.ComponentModel.DataAnnotations;

namespace VerveClothingApi.DTOs
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public decimal BasePrice { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int MaterialId { get; set; }
        public string MaterialName { get; set; }
        public ICollection<string> Categories { get; set; }
        public int AvailableStock { get; set; }
    }

    public class CreateProductDto
    {
        [Required]
        [StringLength(100)]
        public required string Name { get; set; }

        [StringLength(1000)]
        public required string Description { get; set; }

        [Required]
        [Range(0.01, 10000)]
        public decimal BasePrice { get; set; }
        public bool IsActive { get; set; }
        [Required]
        public int MaterialId { get; set; }
        [Required]
        public List<int> CategoryIds { get; set; } = new();
    }

    public class UpdateProductDto
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public decimal? BasePrice { get; set; }
        public bool? IsActive { get; set; }
        public int? MaterialId { get; set; }
    }
}
