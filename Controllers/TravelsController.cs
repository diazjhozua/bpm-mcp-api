using Microsoft.AspNetCore.Mvc;
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

        // Dummy data for travel requests
        private static readonly List<TravelRequest> TravelRequests = new List<TravelRequest>
        {
            new TravelRequest
            {
                Id = 1,
                Type = "Business",
                Purpose = "Client meeting and conference",
                DepartureCity = "New York",
                DepartureDate = DateTime.Now.AddDays(10),
                DestinationCity = "Los Angeles",
                ReturnDate = DateTime.Now.AddDays(15),
                RequestId = "TR-2024-001",
                RequestDate = DateTime.Now.AddDays(-5)
            },
            new TravelRequest
            {
                Id = 2,
                Type = "Training",
                Purpose = "Technical training workshop",
                DepartureCity = "Chicago",
                DepartureDate = DateTime.Now.AddDays(20),
                DestinationCity = "San Francisco",
                ReturnDate = DateTime.Now.AddDays(22),
                RequestId = "TR-2024-002",
                RequestDate = DateTime.Now.AddDays(-2)
            },
            new TravelRequest
            {
                Id = 3,
                Type = "Business",
                Purpose = "Vendor negotiations",
                DepartureCity = "Seattle",
                DepartureDate = DateTime.Now.AddDays(5),
                DestinationCity = "Miami",
                ReturnDate = DateTime.Now.AddDays(7),
                RequestId = "TR-2024-003",
                RequestDate = DateTime.Now.AddDays(-8)
            }
        };

        /// <summary>
        /// Initializes a new instance of the TravelsController
        /// </summary>
        /// <param name="logger">The logger instance</param>
        public TravelsController(ILogger<TravelsController> logger)
        {
            _logger = logger;
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
        public ActionResult<TravelRequest> GetTravelRequest(int id)
        {
            var travelRequest = TravelRequests.FirstOrDefault(tr => tr.Id == id);

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
        public ActionResult<TravelExpense> SubmitTravelExpense([FromBody] TravelExpense travelExpense)
        {
            if (travelExpense == null)
            {
                return BadRequest("Travel expense data is required");
            }

            // Simulate creating the expense with a new ID
            travelExpense.Id = new Random().Next(1000, 9999);

            _logger.LogInformation($"Travel expense {travelExpense.Id} submitted successfully");

            return CreatedAtAction(nameof(SubmitTravelExpense), new { id = travelExpense.Id }, travelExpense);
        }
    }
}