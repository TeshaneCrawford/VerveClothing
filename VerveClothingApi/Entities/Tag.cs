using System.ComponentModel.DataAnnotations;

namespace VerveClothingApi.Entities
{
    public class Tag
    {
        public int TagId { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<ProductTag> ProductTags { get; set; }
    }
}
