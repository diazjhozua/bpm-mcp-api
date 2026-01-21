using Microsoft.EntityFrameworkCore;
using bpm_mcp_api.Models;

namespace bpm_mcp_api.Data
{
    /// <summary>
    /// Database context for the BPM MCP API application
    /// </summary>
    public class BpmDbContext : DbContext
    {
        public BpmDbContext(DbContextOptions<BpmDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Employee expenses table
        /// </summary>
        public DbSet<EmployeeExpense> EmployeeExpenses { get; set; }

        /// <summary>
        /// Travel requests table
        /// </summary>
        public DbSet<TravelRequest> TravelRequests { get; set; }

        /// <summary>
        /// Travel expenses table
        /// </summary>
        public DbSet<TravelExpense> TravelExpenses { get; set; }

        /// <summary>
        /// Assets table
        /// </summary>
        public DbSet<Asset> Assets { get; set; }

        /// <summary>
        /// Asset types table
        /// </summary>
        public DbSet<AssetType> AssetTypes { get; set; }

        /// <summary>
        /// Purchase requests table
        /// </summary>
        public DbSet<PurchaseRequest> PurchaseRequests { get; set; }

        /// <summary>
        /// Purchase request items table
        /// </summary>
        public DbSet<PurchaseRequestItem> PurchaseRequestItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure EmployeeExpense
            modelBuilder.Entity<EmployeeExpense>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.VendorName)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(e => e.Amount)
                    .HasPrecision(10, 2);
                entity.Property(e => e.Currency)
                    .IsRequired()
                    .HasMaxLength(3);
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            // Configure TravelRequest
            modelBuilder.Entity<TravelRequest>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.Purpose)
                    .IsRequired()
                    .HasMaxLength(200);
                entity.Property(e => e.DepartureCity)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(e => e.DestinationCity)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(e => e.RequestId)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            // Configure TravelExpense
            modelBuilder.Entity<TravelExpense>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.VendorName)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(e => e.Amount)
                    .HasPrecision(10, 2);
                entity.Property(e => e.Currency)
                    .IsRequired()
                    .HasMaxLength(3);
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(500);

                // Foreign key relationship
                entity.HasOne<TravelRequest>()
                    .WithMany()
                    .HasForeignKey(e => e.TravelRequestId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure Asset
            modelBuilder.Entity<Asset>(entity =>
            {
                entity.HasKey(e => e.AssetNo);
                entity.Property(e => e.AssetNo)
                    .HasMaxLength(20);
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(200);
                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.Employee)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            // Configure AssetType
            modelBuilder.Entity<AssetType>(entity =>
            {
                entity.HasKey(e => e.ProductId);
                entity.Property(e => e.ProductId)
                    .HasMaxLength(50);
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(200);
                entity.Property(e => e.Specs)
                    .HasMaxLength(500);
                entity.Property(e => e.Price)
                    .HasPrecision(10, 2);
            });

            // Configure PurchaseRequest
            modelBuilder.Entity<PurchaseRequest>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Employee)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.Requestor)
                    .IsRequired()
                    .HasMaxLength(50);

                // Configure relationship with items
                entity.HasMany(e => e.Items)
                    .WithOne()
                    .HasForeignKey("PurchaseRequestId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure PurchaseRequestItem
            modelBuilder.Entity<PurchaseRequestItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ProductId)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.Price)
                    .HasPrecision(10, 2);
                entity.Property("PurchaseRequestId"); // Shadow property for foreign key
            });

            // Seed data for demonstration
            SeedData(modelBuilder);
        }

        private static void SeedData(ModelBuilder modelBuilder)
        {
            // Seed AssetTypes
            modelBuilder.Entity<AssetType>().HasData(
                new AssetType
                {
                    ProductId = "LAPTOP-DL-7420",
                    Description = "Dell Latitude 7420 Business Laptop",
                    Specs = "Intel i7-1185G7, 16GB RAM, 512GB SSD, 14-inch FHD",
                    Price = 1899.99m
                },
                new AssetType
                {
                    ProductId = "MON-LG-34WN",
                    Description = "LG UltraWide 34-inch Curved Monitor",
                    Specs = "3440x1440 resolution, USB-C, HDR10",
                    Price = 699.99m
                },
                new AssetType
                {
                    ProductId = "DESK-SD-001",
                    Description = "Standing Desk - Electric Height Adjustable",
                    Specs = "48x24 inch surface, Memory settings, Cable management",
                    Price = 599.99m
                },
                new AssetType
                {
                    ProductId = "CHAIR-ERG-01",
                    Description = "Ergonomic Office Chair with Lumbar Support",
                    Specs = "Mesh back, Adjustable armrests, 5-year warranty",
                    Price = 449.99m
                },
                new AssetType
                {
                    ProductId = "PHONE-IP15P",
                    Description = "iPhone 15 Pro",
                    Specs = "128GB, Titanium, A17 Pro chip",
                    Price = 999.99m
                }
            );

            // Seed TravelRequests
            modelBuilder.Entity<TravelRequest>().HasData(
                new TravelRequest
                {
                    Id = 1,
                    Type = "Business",
                    Purpose = "Client meeting and conference",
                    DepartureCity = "New York",
                    DepartureDate = DateTime.Now.AddDays(10),
                    DestinationCity = "Los Angeles",
                    ReturnDate = DateTime.Now.AddDays(15),
                    RequestId = "TR-2024-001",
                    RequestDate = DateTime.Now.AddDays(-5)
                },
                new TravelRequest
                {
                    Id = 2,
                    Type = "Training",
                    Purpose = "Technical training workshop",
                    DepartureCity = "Chicago",
                    DepartureDate = DateTime.Now.AddDays(20),
                    DestinationCity = "San Francisco",
                    ReturnDate = DateTime.Now.AddDays(22),
                    RequestId = "TR-2024-002",
                    RequestDate = DateTime.Now.AddDays(-2)
                }
            );

            // Seed Assets
            modelBuilder.Entity<Asset>().HasData(
                new Asset
                {
                    AssetNo = "LAPTOP001",
                    Description = "Dell Latitude 7420 Laptop",
                    Category = "Computer Equipment",
                    Employee = "john.doe",
                    IsForReplacement = false
                },
                new Asset
                {
                    AssetNo = "DESK001",
                    Description = "Standing Desk - Height Adjustable",
                    Category = "Office Furniture",
                    Employee = "john.doe",
                    IsForReplacement = false
                },
                new Asset
                {
                    AssetNo = "MON002",
                    Description = "LG UltraWide 34-inch Monitor",
                    Category = "Computer Equipment",
                    Employee = "jane.smith",
                    IsForReplacement = true
                }
            );
        }
    }
}