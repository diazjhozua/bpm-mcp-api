namespace bpm_mcp_api.Models
{
    /// <summary>
    /// Represents an available asset type for purchase requests
    /// </summary>
    public class AssetType
    {
        /// <summary>
        /// Unique product identifier
        /// </summary>
        /// <example>LAPTOP-DL-7420</example>
        public string ProductId { get; set; } = string.Empty;

        /// <summary>
        /// Product description
        /// </summary>
        /// <example>Dell Latitude 7420 Business Laptop</example>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Technical specifications of the product
        /// </summary>
        /// <example>Intel i7-1185G7, 16GB RAM, 512GB SSD, 14-inch FHD</example>
        public string Specs { get; set; } = string.Empty;

        /// <summary>
        /// Current price of the asset type
        /// </summary>
        /// <example>1899.99</example>
        public decimal Price { get; set; }
    }
}