namespace bpm_mcp_api.Models
{
    /// <summary>
    /// Represents a travel request for business trips
    /// </summary>
    public class TravelRequest
    {
        /// <summary>
        /// Unique identifier for the travel request
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }

        /// <summary>
        /// Type of travel (Business, Training, etc.)
        /// </summary>
        /// <example>Business</example>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Purpose or reason for the travel
        /// </summary>
        /// <example>Client meeting and conference</example>
        public string Purpose { get; set; } = string.Empty;

        /// <summary>
        /// City of departure
        /// </summary>
        /// <example>New York</example>
        public string DepartureCity { get; set; } = string.Empty;

        /// <summary>
        /// Date and time of departure
        /// </summary>
        /// <example>2024-02-01T08:00:00Z</example>
        public DateTime DepartureDate { get; set; }

        /// <summary>
        /// Destination city
        /// </summary>
        /// <example>Los Angeles</example>
        public string DestinationCity { get; set; } = string.Empty;

        /// <summary>
        /// Date and time of return
        /// </summary>
        /// <example>2024-02-06T18:00:00Z</example>
        public DateTime ReturnDate { get; set; }

        /// <summary>
        /// Unique request identifier
        /// </summary>
        /// <example>TR-2024-001</example>
        public string RequestId { get; set; } = string.Empty;

        /// <summary>
        /// Date when the request was submitted
        /// </summary>
        /// <example>2024-01-16T10:30:00Z</example>
        public DateTime RequestDate { get; set; }
    }
}