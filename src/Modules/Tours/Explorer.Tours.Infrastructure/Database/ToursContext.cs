using Explorer.Tours.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Tours.Infrastructure.Database;

public class ToursContext : DbContext
{
    public DbSet<Tour> Tours { get; set; }
    public DbSet<Equipment> Equipment { get; set; }
    public DbSet<KeyPoint> KeyPoint { get; set; }

    public ToursContext(DbContextOptions<ToursContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("tours");
        
        modelBuilder.Entity<Tour>().HasIndex(t => t.Id).IsUnique();
        modelBuilder.Entity<Equipment>().HasIndex(e => e.Id).IsUnique();
        
        modelBuilder.Entity<Tour>()
            .HasMany(t => t.Equipments)
            .WithMany();
        
        ConfigureTour(modelBuilder);
    }

    private static void ConfigureTour(ModelBuilder modelBuilder) {
        //modelBuilder.Entity<Tour>()
        //    .HasOne<Author>()
        //    .WithMany()
        //    .HasForeignKey(k => k.AuthorId)
        //    .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<KeyPoint>()
            .HasOne<Tour>()
            .WithMany() 
            .HasForeignKey(k => k.TourId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}