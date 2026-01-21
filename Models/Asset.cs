namespace bpm_mcp_api.Models
{
    /// <summary>
    /// Represents an asset assigned to an employee
    /// </summary>
    public class Asset
    {
        /// <summary>
        /// Unique asset number identifier
        /// </summary>
        /// <example>LAPTOP001</example>
        public string AssetNo { get; set; } = string.Empty;

        /// <summary>
        /// Description of the asset
        /// </summary>
        /// <example>Dell Latitude 7420 Laptop</example>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Category or type of the asset
        /// </summary>
        /// <example>Computer Equipment</example>
        public string Category { get; set; } = string.Empty;

        /// <summary>
        /// Employee identifier who has the asset assigned
        /// </summary>
        /// <example>john.doe</example>
        public string Employee { get; set; } = string.Empty;

        /// <summary>
        /// Indicates if this asset is marked for replacement
        /// </summary>
        /// <example>false</example>
        public bool IsForReplacement { get; set; }
    }
}