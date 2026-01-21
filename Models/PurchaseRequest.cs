namespace bpm_mcp_api.Models
{
    /// <summary>
    /// Represents a purchase request for multiple asset types
    /// </summary>
    public class PurchaseRequest
    {
        /// <summary>
        /// Unique identifier for the purchase request
        /// </summary>
        /// <example>12345</example>
        public int Id { get; set; }

        /// <summary>
        /// Employee identifier for whom the assets are being requested
        /// </summary>
        /// <example>john.doe</example>
        public string Employee { get; set; } = string.Empty;

        /// <summary>
        /// Identifier of the person making the request
        /// </summary>
        /// <example>jane.manager</example>
        public string Requestor { get; set; } = string.Empty;

        /// <summary>
        /// List of items being requested for purchase
        /// </summary>
        public List<PurchaseRequestItem> Items { get; set; } = new List<PurchaseRequestItem>();
    }

    /// <summary>
    /// Represents an individual item within a purchase request
    /// </summary>
    public class PurchaseRequestItem
    {
        /// <summary>
        /// Unique identifier for the purchase request item
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }

        /// <summary>
        /// Product identifier from the asset types catalog
        /// </summary>
        /// <example>LAPTOP-DL-7420</example>
        public string ProductId { get; set; } = string.Empty;

        /// <summary>
        /// Price per unit of the item
        /// </summary>
        /// <example>1899.99</example>
        public decimal Price { get; set; }

        /// <summary>
        /// Quantity of items being requested
        /// </summary>
        /// <example>2</example>
        public int Quantity { get; set; }
    }
}