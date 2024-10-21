using Explorer.Tours.Core.Domain;
using Explorer.Stakeholders.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Explorer.Stakeholders.Core.Domain;


namespace Explorer.Tours.Infrastructure.Database;

public class ToursContext : DbContext
{
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

        modelBuilder.Entity<Tour>().HasKey(t => t.Id);
        modelBuilder.Entity<Equipment>()
            .ToTable("Equipment", "tours")
            .HasKey(e => e.Id);

            
        modelBuilder.Entity<KeyPoint>().HasKey(k => k.Id);
        modelBuilder.Entity<TourEquipment>().HasKey(te => new { te.TourId, te.EquipmentId });
        
        ConfigureTour(modelBuilder);
        ConfigurePreference(modelBuilder);
        ConfigureTourEquipment(modelBuilder);
        ConfigureTouristEquipment(modelBuilder);
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

		modelBuilder.Entity<TouristEquipment>()
			.ToTable("TouristEquipment", "tours")
			.HasKey(te => new { te.TouristId, te.EquipmentId });

		modelBuilder.Entity<TouristEquipment>()
		   .HasOne<User>()
		   .WithMany()
		   .HasForeignKey(te => te.TouristId)
		   .HasPrincipalKey(u => u.Id);


		modelBuilder.Entity<TouristEquipment>()
			.HasOne<Equipment>()
			.WithMany()
			.HasForeignKey(te => te.EquipmentId)
			.HasPrincipalKey(e => e.Id);

        
	}
}
