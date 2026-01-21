using System.ComponentModel.DataAnnotations;

namespace bpm_mcp_api.Requests
{
    /// <summary>
    /// Request model for submitting travel expenses
    /// </summary>
    public class SubmitTravelExpenseRequest
    {
        /// <summary>
        /// Reference to the associated travel request
        /// </summary>
        /// <example>1</example>
        [Required(ErrorMessage = "Travel request ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Travel request ID must be greater than zero")]
        public int TravelRequestId { get; set; }

        /// <summary>
        /// Name of the vendor or service provider
        /// </summary>
        /// <example>Marriott Hotel</example>
        [Required(ErrorMessage = "Vendor name is required")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Vendor name must be between 1 and 100 characters")]
        public string VendorName { get; set; } = string.Empty;

        /// <summary>
        /// Expense amount (must be greater than zero)
        /// </summary>
        /// <example>250.00</example>
        [Required(ErrorMessage = "Amount is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Date of the invoice
        /// </summary>
        /// <example>2024-02-02T00:00:00Z</example>
        [Required(ErrorMessage = "Invoice date is required")]
        [DataType(DataType.Date)]
        public DateTime InvoiceDate { get; set; }

        /// <summary>
        /// Currency code for the expense (3 character ISO code)
        /// </summary>
        /// <example>USD</example>
        [Required(ErrorMessage = "Currency is required")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Currency must be a 3-character ISO code")]
        [RegularExpression(@"^[A-Z]{3}$", ErrorMessage = "Currency must be a valid 3-character uppercase ISO code")]
        public string Currency { get; set; } = string.Empty;

        /// <summary>
        /// Description of the travel expense
        /// </summary>
        /// <example>Hotel accommodation for business trip</example>
        [Required(ErrorMessage = "Description is required")]
        [StringLength(500, MinimumLength = 5, ErrorMessage = "Description must be between 5 and 500 characters")]
        public string Description { get; set; } = string.Empty;
    }
}