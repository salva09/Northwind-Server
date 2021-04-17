using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace northwind_server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GET(int id)
        {
            var service = new ProductService();

            if (!service.Exists(id))
            {
                return NotFound();
            }

            var product = service.GetProductbyId(id);
            return product;
        }

        [HttpPut("{id}")]
        public ActionResult PUT(int id, UpdateProduct product)
        {
            var service = new ProductService();

            if (!service.Exists(id))
            {
                return NotFound();
            }

            service.UpdateProduct(id, product);
            return NoContent();
        }

        [HttpPost]
        public ActionResult POST(NewProduct product)
        {
            var service = new ProductService();

            service.AddProduct(product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DELETE(int id)
        {
            var service = new ProductService();

            if (!service.Exists(id))
            {
                return NotFound();
            }

            service.DeleteProduct(id);
            return NoContent();
        }
    }
}