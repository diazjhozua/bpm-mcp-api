using Microsoft.AspNetCore.Mvc;
using bpm_mcp_api.Models;
using bpm_mcp_api.Requests;
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
        /// Submits a new employee expense for processing
        /// </summary>
        /// <param name="request">The employee expense data to submit</param>
        /// <returns>The created employee expense with assigned ID</returns>
        /// <response code="201">Returns the newly created employee expense</response>
        /// <response code="400">If the employee expense data is invalid or missing required fields</response>
        [HttpPost("expenses")]
        [ProducesResponseType(typeof(EmployeeExpense), 201)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        [SwaggerOperation(
            Summary = "Submit employee expense",
            Description = "Creates a new employee expense record for processing. Automatically validates vendor information, amounts, currencies, descriptions, and invoice dates using data annotations."
        )]
        public ActionResult<EmployeeExpense> SubmitExpense([FromBody] SubmitEmployeeExpenseRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Map request to response model
            var employeeExpense = new EmployeeExpense
            {
                Id = new Random().Next(1000, 9999), // Simulate creating with new ID
                VendorName = request.VendorName,
                Amount = request.Amount,
                InvoiceDate = request.InvoiceDate,
                Currency = request.Currency,
                Description = request.Description
            };

            _logger.LogInformation($"Employee expense {employeeExpense.Id} submitted successfully for vendor {employeeExpense.VendorName}");

            return CreatedAtAction(nameof(SubmitExpense), new { id = employeeExpense.Id }, employeeExpense);
        }
    }
}