using DependencyInjection.Sample.TightlyCoupled.Core;
using Microsoft.AspNetCore.Mvc;

namespace DependencyInjection.Sample.TightlyCoupled.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductsController()
        {
            _productService = new ProductService();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _productService.GetProducts();
            return Ok(products);
        }
    }
}
