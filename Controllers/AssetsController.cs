using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bpm_mcp_api.Data;
using bpm_mcp_api.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace bpm_mcp_api.Controllers
{
    /// <summary>
    /// Controller for managing asset-related operations including employee assets and asset types
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AssetsController : ControllerBase
    {
        private readonly ILogger<AssetsController> _logger;
        private readonly BpmDbContext _context;

        /// <summary>
        /// Initializes a new instance of the AssetsController
        /// </summary>
        /// <param name="logger">The logger instance</param>
        /// <param name="context">The database context</param>
        public AssetsController(ILogger<AssetsController> logger, BpmDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// Retrieves assets assigned to a specific employee
        /// </summary>
        /// <param name="employee">The employee identifier to filter assets by</param>
        /// <returns>A list of assets assigned to the specified employee</returns>
        /// <response code="200">Returns the list of assets for the employee</response>
        /// <response code="400">If the employee parameter is not provided</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Asset>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [SwaggerOperation(
            Summary = "Get assets by employee",
            Description = "Retrieves all assets assigned to a specific employee including asset numbers, descriptions, categories, and replacement status."
        )]
        public async Task<ActionResult<IEnumerable<Asset>>> GetAssetsByEmployee([FromQuery] string employee)
        {
            if (string.IsNullOrWhiteSpace(employee))
            {
                return BadRequest("Employee parameter is required");
            }

            var employeeAssets = await _context.Assets
                .Where(a => a.Employee.ToLower() == employee.ToLower())
                .ToListAsync();

            return Ok(employeeAssets);
        }

        /// <summary>
        /// Retrieves all available asset types for purchase requests
        /// </summary>
        /// <returns>A list of all available asset types with specifications and pricing</returns>
        /// <response code="200">Returns the list of available asset types</response>
        [HttpGet("types")]
        [ProducesResponseType(typeof(IEnumerable<AssetType>), 200)]
        [SwaggerOperation(
            Summary = "Get available asset types",
            Description = "Retrieves a comprehensive list of all available asset types including product IDs, descriptions, specifications, and current pricing information."
        )]
        public async Task<ActionResult<IEnumerable<AssetType>>> GetAssetTypes()
        {
            var assetTypes = await _context.AssetTypes.ToListAsync();
            return Ok(assetTypes);
        }
    }
}