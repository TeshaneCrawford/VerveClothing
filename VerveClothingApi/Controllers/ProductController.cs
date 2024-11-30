using Microsoft.AspNetCore.Mvc;
using VerveClothingApi.DTOs;
using VerveClothingApi.Exceptions;
using VerveClothingApi.Services;
using VerveClothingApi.Common;
using System.ComponentModel.DataAnnotations;

namespace VerveClothingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(PagedResult<ProductDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProducts([FromQuery] ProductFilterParams filterParams)
        {
            var products = await _productService.GetProductsAsync(filterParams);
            return Ok(products);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchProducts([FromQuery] string query)
        {
            var products = await _productService.SearchProductsAsync(query);
            return Ok(products);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                return Ok(product);
            }
            catch (ProductNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto createProductDto)
        {
            try
            {
                var product = await _productService.CreateProductAsync(createProductDto);
                _logger.LogInformation("Product created: {ProductId}", product.ProductId);
                return CreatedAtAction(nameof(GetProductById), new { id = product.ProductId }, product);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new ErrorResponse(ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating product");
                return StatusCode(500, new ErrorResponse("An error occurred while creating the product"));
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductDto updateProductDto)
        {
            var product = await _productService.UpdateProductAsync(id, updateProductDto);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productService.DeleteProductAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

        public class ErrorResponse
        {
            public string Message { get; set; }
            public ErrorResponse(string message) => Message = message;
        }
    }
}
