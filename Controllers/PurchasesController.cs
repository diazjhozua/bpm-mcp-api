using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bpm_mcp_api.Data;
using bpm_mcp_api.Models;
using bpm_mcp_api.Requests;
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
        private readonly BpmDbContext _context;

        /// <summary>
        /// Initializes a new instance of the PurchasesController
        /// </summary>
        /// <param name="logger">The logger instance</param>
        /// <param name="context">The database context</param>
        public PurchasesController(ILogger<PurchasesController> logger, BpmDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// Creates a new purchase request for asset procurement
        /// </summary>
        /// <param name="request">The purchase request data including employee, requestor, and list of items</param>
        /// <returns>The created purchase request with assigned ID</returns>
        /// <response code="201">Returns the newly created purchase request</response>
        /// <response code="400">If the purchase request data is invalid or missing required fields</response>
        [HttpPost("requests")]
        [ProducesResponseType(typeof(PurchaseRequest), 201)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        [SwaggerOperation(
            Summary = "Create purchase request",
            Description = "Creates a new purchase request for multiple asset types with validation for employee information, requestor details, and item specifications including product IDs, quantities, and pricing."
        )]
        public async Task<ActionResult<PurchaseRequest>> CreatePurchaseRequest([FromBody] CreatePurchaseRequestRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validate that ProductIds exist in AssetTypes
            foreach (var item in request.Items)
            {
                var assetTypeExists = await _context.AssetTypes.AnyAsync(at => at.ProductId == item.ProductId);
                if (!assetTypeExists)
                {
                    return BadRequest($"Asset type with ProductId '{item.ProductId}' not found");
                }
            }

            // Map request to entity models
            var purchaseRequest = new PurchaseRequest
            {
                Employee = request.Employee,
                Requestor = request.Requestor,
                Items = request.Items.Select(item => new PurchaseRequestItem
                {
                    ProductId = item.ProductId,
                    Price = item.Price,
                    Quantity = item.Quantity
                }).ToList()
            };

            // Save to database
            _context.PurchaseRequests.Add(purchaseRequest);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Purchase request {purchaseRequest.Id} created successfully for employee {purchaseRequest.Employee}");

            return CreatedAtAction(nameof(CreatePurchaseRequest), new { id = purchaseRequest.Id }, purchaseRequest);
        }
    }
}