using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace northwind_server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(ILogger<EmployeeController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{id}")]
        public ActionResult<Employee> GET(int id)
        {
            var service = new EmployeeService();
            
            if (!service.Exists(id))
            {
                return NotFound();
            }

            var employee = service.GetEmployeeById(id);
            return employee;
        }

        [HttpPut("{id}")]
        public ActionResult PUT(int id, UpdateEmployee modifiedEmployee)
        {
            var service = new EmployeeService();

            if (!service.Exists(id))
            {
                return NotFound();
            }

            service.UpdateEmployee(id, modifiedEmployee);

            return NoContent();
        }

        [HttpPost]
        public ActionResult POST(NewEmployee employee)
        {
            var service = new EmployeeService();

            service.AddEmployee(employee);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DELETE(int id)
        {
            var service = new EmployeeService();

            if (!service.Exists(id))
            {
                return NotFound();
            }

            service.DeleteEmployeeById(id);
            return NoContent();
        }
    }
}
