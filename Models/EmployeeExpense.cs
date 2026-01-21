namespace bpm_mcp_api.Models
{
    /// <summary>
    /// Represents an employee expense record
    /// </summary>
    public class EmployeeExpense
    {
        /// <summary>
        /// Unique identifier for the expense
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }

        /// <summary>
        /// Name of the vendor or service provider
        /// </summary>
        /// <example>Office Supplies Inc.</example>
        public string VendorName { get; set; } = string.Empty;

        /// <summary>
        /// Expense amount
        /// </summary>
        /// <example>145.75</example>
        public decimal Amount { get; set; }

        /// <summary>
        /// Date of the invoice
        /// </summary>
        /// <example>2024-01-15T00:00:00Z</example>
        public DateTime InvoiceDate { get; set; }

        /// <summary>
        /// Currency code for the expense
        /// </summary>
        /// <example>USD</example>
        public string Currency { get; set; } = string.Empty;

        /// <summary>
        /// Description of the expense
        /// </summary>
        /// <example>Office supplies and stationery</example>
        public string Description { get; set; } = string.Empty;
    }
}