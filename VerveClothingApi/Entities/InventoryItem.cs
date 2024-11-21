namespace VerveClothingApi.Entities
{
    public class InventoryItem
    {
        public int InventoryId { get; set; }
        public int VariantId { get; set; }
        public ProductVariant ProductVariant { get; set; }
        public int Quantity { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
