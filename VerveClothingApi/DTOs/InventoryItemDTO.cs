using System.ComponentModel.DataAnnotations;

namespace VerveClothingApi.DTOs
{
    public class InventoryItemDto
    {
        public int InventoryId { get; set; }
        public int VariantId { get; set; }
        public int Quantity { get; set; }
        public DateTime LastUpdated { get; set; }
    }

    public class CreateInventoryItemDto
    {
        [Required]
        public int VariantId { get; set; }
        [Required]
        public int Quantity { get; set; }
    }

    public class UpdateInventoryItemDto
    {
        [Required]
        public int Quantity { get; set; }
    }
}
