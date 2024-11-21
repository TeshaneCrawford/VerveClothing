namespace VerveClothingApi.Entities
{
    public class WishlistItem
    {
        public int WishlistItemId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public DateTime AddedDate { get; set; }
    }
}
