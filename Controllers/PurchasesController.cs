using Microsoft.AspNetCore.Mvc;
using bpm_mcp_api.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace bpm_mcp_api.Controllers
{
    /// <summary>
    /// Controller for managing purchase request operations
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class PurchasesController : ControllerBase
    {
        private readonly ILogger<PurchasesController> _logger;

        /// <summary>
        /// Initializes a new instance of the PurchasesController
        /// </summary>
        /// <param name="logger">The logger instance</param>
        public PurchasesController(ILogger<PurchasesController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Creates a new purchase request for asset procurement
        /// </summary>
        /// <param name="purchaseRequest">The purchase request data including employee, requestor, and list of items</param>
        /// <returns>The created purchase request with assigned ID</returns>
        /// <response code="201">Returns the newly created purchase request</response>
        /// <response code="400">If the purchase request data is invalid or missing required fields</response>
        [HttpPost("requests")]
        [ProducesResponseType(typeof(PurchaseRequest), 201)]
        [ProducesResponseType(typeof(string), 400)]
        [SwaggerOperation(
            Summary = "Create purchase request",
            Description = "Creates a new purchase request for multiple asset types with validation for employee information, requestor details, and item specifications including product IDs, quantities, and pricing."
        )]
        public ActionResult<PurchaseRequest> CreatePurchaseRequest([FromBody] PurchaseRequest purchaseRequest)
        {
            if (purchaseRequest == null)
            {
                return BadRequest("Purchase request data is required");
            }

            if (string.IsNullOrWhiteSpace(purchaseRequest.Employee))
            {
                return BadRequest("Employee is required");
            }

            if (string.IsNullOrWhiteSpace(purchaseRequest.Requestor))
            {
                return BadRequest("Requestor is required");
            }

            if (purchaseRequest.Items == null || !purchaseRequest.Items.Any())
            {
                return BadRequest("At least one item is required in the purchase request");
            }

            // Validate each item
            foreach (var item in purchaseRequest.Items)
            {
                if (string.IsNullOrWhiteSpace(item.ProductId))
                {
                    return BadRequest("ProductId is required for all items");
                }

                if (item.Price <= 0)
                {
                    return BadRequest("Price must be greater than zero for all items");
                }

                if (item.Quantity <= 0)
                {
                    return BadRequest("Quantity must be greater than zero for all items");
                }
            }

            // Simulate creating the purchase request with a new ID
            purchaseRequest.Id = new Random().Next(10000, 99999);

            _logger.LogInformation($"Purchase request {purchaseRequest.Id} created successfully for employee {purchaseRequest.Employee}");

            return CreatedAtAction(nameof(CreatePurchaseRequest), new { id = purchaseRequest.Id }, purchaseRequest);
        }
    }
}