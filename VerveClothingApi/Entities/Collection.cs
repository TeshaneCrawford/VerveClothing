using System.ComponentModel.DataAnnotations;

namespace VerveClothingApi.Entities
{
    public class Collection
    {
        public int CollectionId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ICollection<CollectionProduct> CollectionProducts { get; set; }
    }
}
