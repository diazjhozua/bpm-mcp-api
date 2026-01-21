using System.ComponentModel.DataAnnotations;

namespace bpm_mcp_api.Requests
{
    /// <summary>
    /// Request model for creating purchase requests
    /// </summary>
    public class CreatePurchaseRequestRequest
    {
        /// <summary>
        /// Employee identifier for whom the assets are being requested
        /// </summary>
        /// <example>john.doe</example>
        [Required(ErrorMessage = "Employee is required")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Employee must be between 1 and 50 characters")]
        public string Employee { get; set; } = string.Empty;

        /// <summary>
        /// Identifier of the person making the request
        /// </summary>
        /// <example>jane.manager</example>
        [Required(ErrorMessage = "Requestor is required")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Requestor must be between 1 and 50 characters")]
        public string Requestor { get; set; } = string.Empty;

        /// <summary>
        /// List of items being requested for purchase
        /// </summary>
        [Required(ErrorMessage = "Items are required")]
        [MinLength(1, ErrorMessage = "At least one item is required")]
        public List<CreatePurchaseRequestItemRequest> Items { get; set; } = new List<CreatePurchaseRequestItemRequest>();
    }

    /// <summary>
    /// Request model for individual purchase request items
    /// </summary>
    public class CreatePurchaseRequestItemRequest
    {
        /// <summary>
        /// Product identifier from the asset types catalog
        /// </summary>
        /// <example>LAPTOP-DL-7420</example>
        [Required(ErrorMessage = "ProductId is required")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "ProductId must be between 1 and 50 characters")]
        public string ProductId { get; set; } = string.Empty;

        /// <summary>
        /// Price per unit of the item
        /// </summary>
        /// <example>1899.99</example>
        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero")]
        public decimal Price { get; set; }

        /// <summary>
        /// Quantity of items being requested
        /// </summary>
        /// <example>2</example>
        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }
    }
}