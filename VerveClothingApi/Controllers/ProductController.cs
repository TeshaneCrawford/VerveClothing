using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VerveClothingApi.DTOs;
using VerveClothingApi.Services;

namespace VerveClothingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto createProductDto)
        {
            var product = await _productService.CreateProductAsync(createProductDto);
            return CreatedAtAction(nameof(GetProductById), new { id = product.ProductId }, product);
        }

        [HttpPost]
        [Route("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductDto updateProductDto)
        {
            var product = await _productService.UpdateProductAsync(id, updateProductDto);
            return Ok(product);
        }
    }
}
