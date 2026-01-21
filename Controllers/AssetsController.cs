using Microsoft.AspNetCore.Mvc;
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

        // Dummy data for assets
        private static readonly List<Asset> Assets = new List<Asset>
        {
            new Asset
            {
                AssetNo = "LAPTOP001",
                Description = "Dell Latitude 7420 Laptop",
                Category = "Computer Equipment",
                Employee = "john.doe",
                IsForReplacement = false
            },
            new Asset
            {
                AssetNo = "DESK001",
                Description = "Standing Desk - Height Adjustable",
                Category = "Office Furniture",
                Employee = "john.doe",
                IsForReplacement = false
            },
            new Asset
            {
                AssetNo = "MON002",
                Description = "LG UltraWide 34-inch Monitor",
                Category = "Computer Equipment",
                Employee = "jane.smith",
                IsForReplacement = true
            },
            new Asset
            {
                AssetNo = "PHONE003",
                Description = "iPhone 15 Pro",
                Category = "Mobile Devices",
                Employee = "jane.smith",
                IsForReplacement = false
            },
            new Asset
            {
                AssetNo = "CHAIR001",
                Description = "Ergonomic Office Chair",
                Category = "Office Furniture",
                Employee = "bob.wilson",
                IsForReplacement = true
            }
        };

        // Dummy data for asset types
        private static readonly List<AssetType> AssetTypes = new List<AssetType>
        {
            new AssetType
            {
                ProductId = "LAPTOP-DL-7420",
                Description = "Dell Latitude 7420 Business Laptop",
                Specs = "Intel i7-1185G7, 16GB RAM, 512GB SSD, 14-inch FHD",
                Price = 1899.99m
            },
            new AssetType
            {
                ProductId = "MON-LG-34WN",
                Description = "LG UltraWide 34-inch Curved Monitor",
                Specs = "3440x1440 resolution, USB-C, HDR10",
                Price = 699.99m
            },
            new AssetType
            {
                ProductId = "DESK-SD-001",
                Description = "Standing Desk - Electric Height Adjustable",
                Specs = "48x24 inch surface, Memory settings, Cable management",
                Price = 599.99m
            },
            new AssetType
            {
                ProductId = "CHAIR-ERG-01",
                Description = "Ergonomic Office Chair with Lumbar Support",
                Specs = "Mesh back, Adjustable armrests, 5-year warranty",
                Price = 449.99m
            },
            new AssetType
            {
                ProductId = "PHONE-IP15P",
                Description = "iPhone 15 Pro",
                Specs = "128GB, Titanium, A17 Pro chip",
                Price = 999.99m
            }
        };

        /// <summary>
        /// Initializes a new instance of the AssetsController
        /// </summary>
        /// <param name="logger">The logger instance</param>
        public AssetsController(ILogger<AssetsController> logger)
        {
            _logger = logger;
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
        public ActionResult<IEnumerable<Asset>> GetAssetsByEmployee([FromQuery] string employee)
        {
            if (string.IsNullOrWhiteSpace(employee))
            {
                return BadRequest("Employee parameter is required");
            }

            var employeeAssets = Assets.Where(a => a.Employee.Equals(employee, StringComparison.OrdinalIgnoreCase)).ToList();

            if (!employeeAssets.Any())
            {
                return Ok(new List<Asset>());
            }

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
        public ActionResult<IEnumerable<AssetType>> GetAssetTypes()
        {
            return Ok(AssetTypes);
        }
    }
}