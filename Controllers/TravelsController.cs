using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bpm_mcp_api.Data;
using bpm_mcp_api.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace bpm_mcp_api.Controllers
{
    /// <summary>
    /// Controller for managing travel-related operations including travel requests and expenses
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class TravelsController : ControllerBase
    {
        private readonly ILogger<TravelsController> _logger;
        private readonly BpmDbContext _context;

        /// <summary>
        /// Initializes a new instance of the TravelsController
        /// </summary>
        /// <param name="logger">The logger instance</param>
        /// <param name="context">The database context</param>
        public TravelsController(ILogger<TravelsController> logger, BpmDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// Retrieves a specific travel request by its ID
        /// </summary>
        /// <param name="id">The unique identifier of the travel request</param>
        /// <returns>The travel request details if found</returns>
        /// <response code="200">Returns the travel request</response>
        /// <response code="404">If the travel request is not found</response>
        [HttpGet("requests/{id}")]
        [ProducesResponseType(typeof(TravelRequest), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [SwaggerOperation(
            Summary = "Get travel request by ID",
            Description = "Retrieves detailed information about a specific travel request including departure/destination cities, dates, purpose, and request information."
        )]
        public async Task<ActionResult<TravelRequest>> GetTravelRequest(int id)
        {
            var travelRequest = await _context.TravelRequests.FindAsync(id);

            if (travelRequest == null)
            {
                return NotFound($"Travel request with ID {id} not found");
            }

            return Ok(travelRequest);
        }

        /// <summary>
        /// Submits a new travel expense
        /// </summary>
        /// <param name="travelExpense">The travel expense data to submit</param>
        /// <returns>The created travel expense with assigned ID</returns>
        /// <response code="201">Returns the newly created travel expense</response>
        /// <response code="400">If the travel expense data is invalid</response>
        [HttpPost("expenses")]
        [ProducesResponseType(typeof(TravelExpense), 201)]
        [ProducesResponseType(typeof(string), 400)]
        [SwaggerOperation(
            Summary = "Submit travel expense",
            Description = "Creates a new travel expense record associated with a travel request, including vendor information, amount, and expense details."
        )]
        public async Task<ActionResult<TravelExpense>> SubmitTravelExpense([FromBody] TravelExpense travelExpense)
        {
            if (travelExpense == null)
            {
                return BadRequest("Travel expense data is required");
            }

            // Validate that the TravelRequestId exists
            var travelRequestExists = await _context.TravelRequests.AnyAsync(tr => tr.Id == travelExpense.TravelRequestId);
            if (!travelRequestExists)
            {
                return BadRequest($"Travel request with ID {travelExpense.TravelRequestId} not found");
            }

            // Save to database
            _context.TravelExpenses.Add(travelExpense);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Travel expense {travelExpense.Id} submitted successfully");

            return CreatedAtAction(nameof(SubmitTravelExpense), new { id = travelExpense.Id }, travelExpense);
        }
    }
}