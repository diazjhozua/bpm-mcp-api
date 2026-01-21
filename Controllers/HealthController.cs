using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bpm_mcp_api.Data;
using Swashbuckle.AspNetCore.Annotations;

namespace bpm_mcp_api.Controllers
{
    /// <summary>
    /// Controller for system health checks and monitoring
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class HealthController : ControllerBase
    {
        private readonly ILogger<HealthController> _logger;
        private readonly BpmDbContext _context;

        /// <summary>
        /// Initializes a new instance of the HealthController
        /// </summary>
        /// <param name="logger">The logger instance</param>
        /// <param name="context">The database context</param>
        public HealthController(ILogger<HealthController> logger, BpmDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// Checks the overall health of the application including database connectivity
        /// </summary>
        /// <returns>Health status information including database connection and record counts</returns>
        /// <response code="200">Application is healthy and database is connected</response>
        /// <response code="503">Application or database connection issues detected</response>
        [HttpGet]
        [ProducesResponseType(typeof(HealthCheckResponse), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 503)]
        [SwaggerOperation(
            Summary = "Application Health Check",
            Description = "Performs a comprehensive health check of the application including database connectivity, basic queries, and system status. Returns detailed information about connection status and database record counts for verification."
        )]
        public async Task<ActionResult<HealthCheckResponse>> CheckHealth()
        {
            try
            {
                _logger.LogInformation("Health check started at {Timestamp}", DateTime.UtcNow);

                // Test database connection
                var canConnect = await _context.Database.CanConnectAsync();
                var connectionString = _context.Database.GetConnectionString();

                if (canConnect)
                {
                    // Try simple queries to verify database functionality
                    var assetCount = await _context.AssetTypes.CountAsync();
                    var employeeExpenseCount = await _context.EmployeeExpenses.CountAsync();
                    var travelRequestCount = await _context.TravelRequests.CountAsync();

                    var response = new HealthCheckResponse
                    {
                        Status = "Healthy",
                        Database = "Connected",
                        ConnectionString = connectionString?.Substring(0, Math.Min(50, connectionString.Length)) + "...",
                        DatabaseCounts = new DatabaseCounts
                        {
                            AssetTypes = assetCount,
                            EmployeeExpenses = employeeExpenseCount,
                            TravelRequests = travelRequestCount
                        },
                        Timestamp = DateTime.UtcNow
                    };

                    _logger.LogInformation("Health check completed successfully");
                    return Ok(response);
                }
                else
                {
                    _logger.LogError("Database connection test failed");
                    return StatusCode(503, new ProblemDetails
                    {
                        Title = "Database Connection Failed",
                        Detail = "Unable to connect to the database",
                        Status = 503
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Health check failed with exception: {Message}", ex.Message);
                return StatusCode(503, new ProblemDetails
                {
                    Title = "Health Check Failed",
                    Detail = ex.Message,
                    Status = 503
                });
            }
        }
    }

    /// <summary>
    /// Health check response model
    /// </summary>
    public class HealthCheckResponse
    {
        /// <summary>
        /// Overall health status of the application
        /// </summary>
        /// <example>Healthy</example>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// Database connection status
        /// </summary>
        /// <example>Connected</example>
        public string Database { get; set; } = string.Empty;

        /// <summary>
        /// Partial connection string for verification (sensitive data masked)
        /// </summary>
        /// <example>Server=tcp:myserver.database.windows.net,1433;Initial...</example>
        public string ConnectionString { get; set; } = string.Empty;

        /// <summary>
        /// Database record counts for verification
        /// </summary>
        public DatabaseCounts DatabaseCounts { get; set; } = new DatabaseCounts();

        /// <summary>
        /// Timestamp when the health check was performed
        /// </summary>
        /// <example>2024-02-02T10:30:00Z</example>
        public DateTime Timestamp { get; set; }
    }

    /// <summary>
    /// Database record counts for health verification
    /// </summary>
    public class DatabaseCounts
    {
        /// <summary>
        /// Number of asset types in the database
        /// </summary>
        /// <example>5</example>
        public int AssetTypes { get; set; }

        /// <summary>
        /// Number of employee expenses in the database
        /// </summary>
        /// <example>12</example>
        public int EmployeeExpenses { get; set; }

        /// <summary>
        /// Number of travel requests in the database
        /// </summary>
        /// <example>8</example>
        public int TravelRequests { get; set; }
    }
}