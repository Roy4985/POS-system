using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

class InventoryContext : DbContext
{
    public DbSet<Product> Products {get; set;}
    public DbSet<Location> Locations {get; set;}
    public DbSet<StockEntry> StockEntries {get; set;}
    public DbSet<StockMovement> StockMovements {get; set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) // This is used to conenct to the database
    {
        var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        var connectionString = config.GetConnectionString("Inventory");

        optionsBuilder.UseNpgsql(connectionString).UseSnakeCaseNamingConvention(); // add .LogTo(Console.WriteLine, LogLevel.Information) for debugging
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) // this is used to add propreties and constraints when migrating
    {
    // Naming changes 
    modelBuilder.Entity<Location>().Property(l => l.LType).HasColumnName("type");
    modelBuilder.Entity<Product>().Property(p => p.HasTVA).HasColumnName("has_tva");
    modelBuilder.Entity<StockEntry>().Property(s => s.Row).HasColumnName("row_pos"); // row and column are SQL elements so change the name
    modelBuilder.Entity<StockEntry>().Property(s => s.Column).HasColumnName("col_pos");
    modelBuilder.Entity<StockMovement>().Property(s => s.FromLocationId).HasColumnName("from_id");
    modelBuilder.Entity<StockMovement>().Property(s => s.ToLocationId).HasColumnName("to_id");
    modelBuilder.Entity<StockMovement>().Property(s => s.Timestamp).HasColumnName("created_at"); // Timestamp is also an sql element

    // EF core store Enums as Integer, I will change them to strings and add checks constraits, it will be safer
    modelBuilder.Entity<Location>().Property(l => l.LType).HasConversion<string>();
    modelBuilder.Entity<StockMovement>().Property(s => s.Type).HasConversion<string>();

    // Constraints
    modelBuilder.Entity<StockEntry>().HasIndex(s => new {s.ProductId, s.LocationId}).IsUnique(); // (produt_id and location_id are together are unnique (like composed PK))
    modelBuilder.Entity<StockEntry>().ToTable(s => s.HasCheckConstraint("quantity_not_negative", "quantity >= 0"));
    modelBuilder.Entity<StockMovement>().ToTable(s => s.HasCheckConstraint("different_source_destination", "from_id IS DISTINCT FROM to_id"));
    modelBuilder.Entity<StockMovement>().ToTable(s => s.HasCheckConstraint("movement_quantity_positive", "quantity > 0"));

    // DELETE behavior
    modelBuilder.Entity<StockEntry>()
    .HasOne<Product>()                  // each stock entry relates to ONE PRODUCT (foreign key)
    .WithMany()                         // a product can link to many stock entries
    .HasForeignKey(se => se.ProductId)  // they are linked through ProductId (FK)
    .OnDelete(DeleteBehavior.Restrict); // refuse to delete a Product that has StockEntries (since we are going to use the soft delete system (is_active))

    modelBuilder.Entity<StockEntry>()
    .HasOne<Location>()
    .WithMany()
    .HasForeignKey(se => se.LocationId)
    .OnDelete(DeleteBehavior.Restrict);

    modelBuilder.Entity<StockMovement>()
    .HasOne<Location>()
    .WithMany()
    .HasForeignKey(se => se.FromLocationId)
    .OnDelete(DeleteBehavior.Restrict);

    modelBuilder.Entity<StockMovement>()
    .HasOne<Location>()
    .WithMany()
    .HasForeignKey(se => se.ToLocationId)
    .OnDelete(DeleteBehavior.Restrict);

    modelBuilder.Entity<StockMovement>()
    .HasOne<Product>()
    .WithMany()
    .HasForeignKey(se => se.ProductId)
    .OnDelete(DeleteBehavior.Restrict);

    }

}