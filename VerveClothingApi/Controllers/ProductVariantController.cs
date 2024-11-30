using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VerveClothingApi.DTOs;
using VerveClothingApi.Services;

namespace VerveClothingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductVariantController : ControllerBase
    {
        private readonly IProductVariantService _variantService;

        public ProductVariantController(IProductVariantService variantService)
        {
            _variantService = variantService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductVariantDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllVariants()
        {
            var variants = await _variantService.GetAllProductVariantsAsync();
            return Ok(variants);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductVariantDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetVariantById(int id)
        {
            var variant = await _variantService.GetProductVariantByIdAsync(id);
            if (variant == null) return NotFound();
            return Ok(variant);
        }

        [HttpGet("product/{productId}")]
        [ProducesResponseType(typeof(IEnumerable<ProductVariantDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetVariantsByProductId(int productId)
        {
            var variants = await _variantService.GetProductVariantsByProductIdAsync(productId);
            return Ok(variants);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProductVariantDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateVariant([FromBody] CreateProductVariantDto createDto)
        {
            var variant = await _variantService.CreateProductVariantAsync(createDto);
            return CreatedAtAction(nameof(GetVariantById), new { id = variant.VariantId }, variant);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ProductVariantDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateVariant(int id, [FromBody] UpdateProductVariantDto updateDto)
        {
            var variant = await _variantService.UpdateProductVariantAsync(id, updateDto);
            if (variant == null) return NotFound();
            return Ok(variant);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteVariant(int id)
        {
            var result = await _variantService.DeleteProductVariantAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
