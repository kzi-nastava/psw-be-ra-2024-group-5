using Explorer.Tours.Core.Domain;
using Explorer.Stakeholders.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Tours.Infrastructure.Database;

public class ToursContext : DbContext
{
    public DbSet<TourReview> TourReviews { get; set; }
    public DbSet<Tour> Tours { get; set; }
    public DbSet<Equipment> Equipment { get; set; }
    public DbSet<TourEquipment> TourEquipment { get; set; }
    public DbSet<KeyPoint> KeyPoint { get; set; }
    public DbSet<Facility> Facilities { get; set; }

    public ToursContext(DbContextOptions<ToursContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("tours");

        modelBuilder.Entity<Tour>().HasKey(t => t.Id);

        modelBuilder.Entity<Equipment>().HasKey(e => e.Id);
                
        modelBuilder.Entity<KeyPoint>().HasIndex(k => k.Id).IsUnique();
        ConfigureTour(modelBuilder);
        ConfigureTourEquipment(modelBuilder);
    }

    private static void ConfigureTour(ModelBuilder modelBuilder) {
        modelBuilder.Entity<User>()
            .ToTable("Users", "stakeholders")
            .Metadata.SetIsTableExcludedFromMigrations(true);

        modelBuilder.Entity<Tour>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(t => t.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<KeyPoint>()
            .HasOne<Tour>()
            .WithMany() 
            .HasForeignKey(k => k.TourId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    private static void ConfigureTourEquipment(ModelBuilder modelBuilder) {
        modelBuilder.Entity<TourEquipment>()
            .HasKey(te => new { te.TourId, te.EquipmentId });

        modelBuilder.Entity<TourEquipment>()
            .HasOne<Tour>()
            .WithMany()
            .HasForeignKey(te => te.TourId);

        modelBuilder.Entity<TourEquipment>()
            .HasOne<Equipment>()
            .WithMany()
            .HasForeignKey(te => te.EquipmentId);
    }
}
