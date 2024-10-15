using Explorer.Tours.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Tours.Infrastructure.Database;

public class ToursContext : DbContext
{
    public DbSet<Equipment> Equipment { get; set; }
    public DbSet<KeyPoint> KeyPoint { get; set; }

    public ToursContext(DbContextOptions<ToursContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("tours");
    }

    /* private static void ConfigureStakeholder(ModelBuilder modelBuilder) {
        modelBuilder.Entity<KeyPoint>()
            .HasOne<Tour>()
            .WithOne()
            .HasForeignKey<Person>(s => s.UserId);
    } */
}