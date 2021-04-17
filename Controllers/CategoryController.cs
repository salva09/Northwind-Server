using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace northwind_server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ILogger<CategoryController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{id}")]
        public ActionResult<Category> GET(int id)
        {
            var service = new CategoryService();

            if (!service.Exists(id))
            {
                return NotFound();
            }

            return service.GetCategoryById(id);
        }

        [HttpPut("{id}")]
        public ActionResult PUT(int id, UpdateCategory category)
        {
            var service = new CategoryService();

            if (!service.Exists(id))
            {
                return NotFound();
            }

            service.UpdateCategory(id, category);
            return NoContent();
        }

        [HttpPost]
        public ActionResult POST(NewCategory category)
        {
            var service = new CategoryService();

            service.AddCategory(category);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DELETE(int id)
        {
            var service = new CategoryService();

            service.DeleteCategory(id);

            return NoContent();
        }
    }
}
