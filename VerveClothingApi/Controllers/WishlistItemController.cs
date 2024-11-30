using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VerveClothingApi.DTOs;
using VerveClothingApi.Services;

namespace VerveClothingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistItemController : ControllerBase
    {
        private readonly IWishlistItemService _wishlistItemService;
        private readonly ILogger<WishlistItemController> _logger;

        public WishlistItemController(IWishlistItemService wishlistItemService, ILogger<WishlistItemController> logger)
        {
            _wishlistItemService = wishlistItemService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(WishlistItemDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetWishlistItem(int id)
        {
            var item = await _wishlistItemService.GetWishlistItemByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpGet("user/{userId}")]
        [ProducesResponseType(typeof(IEnumerable<WishlistItemDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserWishlistItems(int userId)
        {
            var items = await _wishlistItemService.GetWishlistItemsByUserIdAsync(userId);
            return Ok(items);
        }

        [HttpPost]
        [ProducesResponseType(typeof(WishlistItemDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddToWishlist([FromBody] CreateWishlistItemDto createWishlistItemDto)
        {
            try
            {
                var item = await _wishlistItemService.AddToWishlistAsync(createWishlistItemDto);
                if (item == null)
                {
                    return BadRequest(new { message = "Item already exists in wishlist" });
                }
                return CreatedAtAction(nameof(GetWishlistItem), new { id = item.WishlistItemId }, item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding item to wishlist");
                return BadRequest(new { message = "Error adding item to wishlist" });
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveFromWishlist(int id)
        {
            var result = await _wishlistItemService.RemoveFromWishlistAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
