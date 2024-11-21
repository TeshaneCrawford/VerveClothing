namespace VerveClothingApi.Entities
{
    public class ShippingAddress
    {
        public int ShippingId { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
    }
}
