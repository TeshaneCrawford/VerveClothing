using System.ComponentModel.DataAnnotations;

namespace VerveClothingApi.DTOs
{
    public class WishlistItemDto
    {
        public int WishlistItemId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public DateTime AddedDate { get; set; }
    }

    public class CreateWishlistItemDto
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int ProductId { get; set; }
    }
}
