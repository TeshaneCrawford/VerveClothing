using System.ComponentModel.DataAnnotations;

namespace VerveClothingApi.Entities
{
    public class Material
    {
        public int MaterialId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
