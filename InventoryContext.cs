using Microsoft.EntityFrameworkCore;

class InventoryContext : DbContext
{
    public DbSet<Product> Products {get; set;}
    public DbSet<Location> Locations {get; set;}
    public DbSet<StockEntry> StockEntries {get; set;}
    public DbSet<StockMovement> StockMovements {get; set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) // This is used to conenct the code to the Database using EF Core
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=InventoryV2;Username=postgres;Password=123");
    }
}