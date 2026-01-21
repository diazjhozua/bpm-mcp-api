using System.ComponentModel.DataAnnotations;
using bpm_mcp_api.Validation;

namespace bpm_mcp_api.Requests
{
    /// <summary>
    /// Request model for submitting employee expenses
    /// </summary>
    public class SubmitEmployeeExpenseRequest
    {
        /// <summary>
        /// Name of the vendor or service provider
        /// </summary>
        /// <example>Office Supplies Inc.</example>
        [Required(ErrorMessage = "Vendor name is required")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Vendor name must be between 1 and 100 characters")]
        public string VendorName { get; set; } = string.Empty;

        /// <summary>
        /// Expense amount (must be greater than zero)
        /// </summary>
        /// <example>145.75</example>
        [Required(ErrorMessage = "Amount is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Date of the invoice (cannot be in the future)
        /// </summary>
        /// <example>2024-01-15T00:00:00Z</example>
        [Required(ErrorMessage = "Invoice date is required")]
        [DataType(DataType.Date)]
        [NotFutureDate(ErrorMessage = "Invoice date cannot be in the future")]
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
        /// Description of the expense
        /// </summary>
        /// <example>Office supplies and stationery</example>
        [Required(ErrorMessage = "Description is required")]
        [StringLength(500, MinimumLength = 5, ErrorMessage = "Description must be between 5 and 500 characters")]
        public string Description { get; set; } = string.Empty;
    }
}