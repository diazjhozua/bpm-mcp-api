namespace bpm_mcp_api.Models
{
    /// <summary>
    /// Represents a travel-related expense submission
    /// </summary>
    public class TravelExpense
    {
        /// <summary>
        /// Unique identifier for the travel expense
        /// </summary>
        /// <example>1001</example>
        public int Id { get; set; }

        /// <summary>
        /// Reference to the associated travel request
        /// </summary>
        /// <example>1</example>
        public int TravelRequestId { get; set; }

        /// <summary>
        /// Name of the vendor or service provider
        /// </summary>
        /// <example>Marriott Hotel</example>
        public string VendorName { get; set; } = string.Empty;

        /// <summary>
        /// Expense amount
        /// </summary>
        /// <example>250.00</example>
        public decimal Amount { get; set; }

        /// <summary>
        /// Date of the invoice
        /// </summary>
        /// <example>2024-02-02T00:00:00Z</example>
        public DateTime InvoiceDate { get; set; }

        /// <summary>
        /// Currency code for the expense
        /// </summary>
        /// <example>USD</example>
        public string Currency { get; set; } = string.Empty;

        /// <summary>
        /// Description of the travel expense
        /// </summary>
        /// <example>Hotel accommodation for business trip</example>
        public string Description { get; set; } = string.Empty;
    }
}