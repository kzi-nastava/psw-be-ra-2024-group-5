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
    public DbSet<Preference> Preferences { get; set; }
	public DbSet<TouristEquipment> TouristEquipment { get; set; }


	public ToursContext(DbContextOptions<ToursContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("tours");

        modelBuilder.Entity<Tour>().HasIndex(t => t.Id).IsUnique();
        modelBuilder.Entity<Tour>().HasKey(t => t.Id);
        modelBuilder.Entity<Equipment>().HasKey(e => e.Id);
        modelBuilder.Entity<KeyPoint>().HasKey(k => k.Id);
        
                
        modelBuilder.Entity<KeyPoint>().HasIndex(k => k.Id).IsUnique();

        modelBuilder.Entity<TourReview>().HasKey(t => t.Id);

        ConfigureTour(modelBuilder);
        ConfigurePreference(modelBuilder);
        ConfigureTourEquipment(modelBuilder);
        ConfigureTouristEquipment(modelBuilder);
        ConfigureTourReview(modelBuilder);
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

    private static void ConfigurePreference(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
           .ToTable("Users", "stakeholders")
           .Metadata.SetIsTableExcludedFromMigrations(true);

        modelBuilder.Entity<Preference>()
            .HasKey(p => p.Id);

        // Konfigurišemo entitet Preference da koristi tabelu Users iz šeme stakeholders
        modelBuilder.Entity<Preference>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(p => p.TouristId)
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
    private static void ConfigureTouristEquipment(ModelBuilder modelBuilder)
    {
		modelBuilder.Entity<User>().ToTable("Users", "stakeholders").Metadata.SetIsTableExcludedFromMigrations(true);

		modelBuilder.Entity<TouristEquipment>().HasKey(te => new { te.TouristId, te.EquipmentId });

        modelBuilder.Entity<TouristEquipment>()
           .HasOne<User>()
           .WithMany()
           .HasForeignKey(te => te.TouristId);


        modelBuilder.Entity<TouristEquipment>()
            .HasOne<Equipment>()
            .WithMany()
            .HasForeignKey(te => te.EquipmentId);

    }

    private static void ConfigureTourReview(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
        .ToTable("Users", "stakeholders")
        .Metadata.SetIsTableExcludedFromMigrations(true);

        modelBuilder.Entity<TourReview>()
        .HasOne<User>()
        .WithMany()
        .HasForeignKey(tr => tr.TouristId)
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TourReview>()
            .HasOne<Tour>()
            .WithMany()
            .HasForeignKey(tr => tr.TourId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TourReview>()
            .Property(tr => tr.Rating)
            .IsRequired();

        modelBuilder.Entity<TourReview>()
            .Property(tr => tr.Comment)
            .IsRequired();

        modelBuilder.Entity<TourReview>()
            .Property(tr => tr.VisitDate)
            .IsRequired();

        modelBuilder.Entity<TourReview>()
            .Property(tr => tr.ReviewDate)
            .IsRequired();
    }
}
