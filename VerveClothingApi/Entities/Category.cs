using System.ComponentModel.DataAnnotations;

namespace VerveClothingApi.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        [Required]
        public string Name { get; set; }
        public int? ParentCategoryId { get; set; }
        public Category ParentCategory { get; set; }

        public ICollection<CategoryProduct> CategoryProducts { get; set; }
    }
}
