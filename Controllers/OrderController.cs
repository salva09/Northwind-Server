using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace northwind_server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;

        public OrderController(ILogger<OrderController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{id}")]
        public ActionResult<Order> GET(int id)
        {
            var service = new OrderService();

            if (!service.Exists(id))
            {
                return NotFound();
            }

            return service.GetOrderById(id);
        }
    }
}
