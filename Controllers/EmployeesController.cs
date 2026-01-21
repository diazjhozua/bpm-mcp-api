using Microsoft.AspNetCore.Mvc;
using bpm_mcp_api.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace bpm_mcp_api.Controllers
{
    /// <summary>
    /// Controller for managing employee-related operations
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class EmployeesController : ControllerBase
    {
        private readonly ILogger<EmployeesController> _logger;

        /// <summary>
        /// Initializes a new instance of the EmployeesController
        /// </summary>
        /// <param name="logger">The logger instance</param>
        public EmployeesController(ILogger<EmployeesController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Retrieves a list of employee expenses
        /// </summary>
        /// <returns>A list of employee expenses with vendor information, amounts, and descriptions</returns>
        /// <response code="200">Returns the list of employee expenses</response>
        [HttpGet("expenses")]
        [ProducesResponseType(typeof(IEnumerable<EmployeeExpense>), 200)]
        [SwaggerOperation(
            Summary = "Get employee expenses",
            Description = "Retrieves a comprehensive list of employee expenses including vendor details, amounts, currencies, and descriptions."
        )]
        public IEnumerable<EmployeeExpense> GetExpenses()
        {
            return new List<EmployeeExpense>
            {
                new EmployeeExpense
                {
                    Id = 1,
                    VendorName = "Office Supplies Inc.",
                    Amount = 145.75m,
                    InvoiceDate = DateTime.Now.AddDays(-5),
                    Currency = "USD",
                    Description = "Office supplies and stationery"
                },
                new EmployeeExpense
                {
                    Id = 2,
                    VendorName = "TechCorp Solutions",
                    Amount = 2299.99m,
                    InvoiceDate = DateTime.Now.AddDays(-10),
                    Currency = "USD",
                    Description = "Laptop repair and maintenance"
                },
                new EmployeeExpense
                {
                    Id = 3,
                    VendorName = "Travel Express",
                    Amount = 350.50m,
                    InvoiceDate = DateTime.Now.AddDays(-3),
                    Currency = "USD",
                    Description = "Business trip transportation"
                },
                new EmployeeExpense
                {
                    Id = 4,
                    VendorName = "Conference Center LLC",
                    Amount = 85.00m,
                    InvoiceDate = DateTime.Now.AddDays(-7),
                    Currency = "USD",
                    Description = "Training workshop registration"
                }
            };
        }
    }
}