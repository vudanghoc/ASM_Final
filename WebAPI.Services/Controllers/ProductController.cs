using Microsoft.AspNetCore.Mvc;
using Services.Contracts.Services;
using Services.Models.Product;

namespace WebAPI.Server.Controllers
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
        // GET: api/<ProductController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var option = await _productService.GetAllProducts();
            return Ok(option);
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var option = await _productService.GetProductById(id);
            return Ok(option);
        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductForCreate productDto)
        {
            var product = await _productService.AddProduct(productDto);
            return Ok(product);
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ProductForUpdate productDto)
        {
            var product = await _productService.UpdateProduct(productDto, id);
            return Ok(product);
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productService.DeleteProduct(id);
            return Ok(product);
        }
    }
}