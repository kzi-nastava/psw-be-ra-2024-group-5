using Explorer.Tours.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Tours.Infrastructure.Database;

public class ToursContext : DbContext
{
    public DbSet<Equipment> Equipment { get; set; }

    public ToursContext(DbContextOptions<ToursContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("tours");

        modelBuilder.Entity<Tour>().HasIndex(t => t.Id).IsUnique();
        modelBuilder.Entity<Equipment>().HasIndex(e => e.Id).IsUnique();
        
        modelBuilder.Entity<Tour>()
            .HasMany(t => t.Equipments)
            .WithMany();
    }
}