using DependencyInjection.Sample.LooselyCoupled.Core;
using Microsoft.AspNetCore.Mvc;

namespace DependencyInjection.Sample.LooselyCoupled.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _productService.GetProducts();
            return Ok(products);
        }
    }
}
