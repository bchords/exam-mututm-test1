using Microsoft.EntityFrameworkCore;

public class FruitDb : DbContext
{
    public FruitDb(DbContextOptions<FruitDb> options) : base(options) { }

    public DbSet<Fruits> Fruits { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Seed some initial data
        modelBuilder.Entity<Fruits>().HasData(
            new Fruits { Id = 1, Name = "Apple", Color = "Red", Price = 1.50m, InStock = true },
            new Fruits { Id = 2, Name = "Banana", Color = "Yellow", Price = 0.80m, InStock = true },
            new Fruits { Id = 3, Name = "Orange", Color = "Orange", Price = 1.20m, InStock = false }
        );
    }
}
