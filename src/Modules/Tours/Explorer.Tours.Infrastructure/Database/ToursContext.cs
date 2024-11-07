using Explorer.Tours.Core.Domain;
using Explorer.Stakeholders.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Explorer.Tours.Core.Domain.ShoppingCarts;


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
    public DbSet<TourExecution> TourExecutions { get; set; }
	public DbSet<TouristEquipment> TouristEquipment { get; set; }
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<TourPurchaseToken> TourPurchaseTokens { get; set; }

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
        ConfigureTourExecution(modelBuilder);
        ConfigurePreference(modelBuilder);
        ConfigureTourEquipment(modelBuilder);
        ConfigureTouristEquipment(modelBuilder);
        ConfigureTourReview(modelBuilder);
        ConfigureShoppingCarts(modelBuilder);
        ConfigureTourPurchaseToken(modelBuilder);
    }

    private static void ConfigureTour(ModelBuilder modelBuilder) {

        modelBuilder.Entity<Tour>()
            .HasMany(sc => sc.KeyPoints)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Tour>()
            .HasMany(sc => sc.Reviews)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Tour>()
            .Property(t => t.Price)
            .HasColumnType("jsonb");

        modelBuilder.Entity<Tour>()
            .Property(t => t.TransportDurations)
            .HasColumnType("jsonb");
    }

    private static void ConfigureTourExecution(ModelBuilder modelBuilder) {

        modelBuilder.Entity<TourExecution>()
            .HasKey(te => te.Id);

        modelBuilder.Entity<TourExecution>()
            .HasOne<Tour>()
            .WithMany()
            .HasForeignKey(te => te.TourId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TourExecution>()
            .HasMany(te => te.KeyPointProgresses)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<KeyPointProgress>()
            .HasOne(kp => kp.KeyPoint)
            .WithOne()
            .HasForeignKey<KeyPointProgress>(kp => kp.KeyPointId)
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

    private static void ConfigureShoppingCarts(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ShoppingCart>()
            .HasKey(sc => sc.Id);

        modelBuilder.Entity<ShoppingCart>()
            .HasMany(sc => sc.Items)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<OrderItem>()
            .HasOne<Tour>()
            .WithMany()
            .HasForeignKey(oi => oi.TourId);

        modelBuilder.Entity<OrderItem>()
            .Property(oi => oi.Price)
            .HasColumnType("jsonb");

        modelBuilder.Entity<ShoppingCart>()
            .Property(sc => sc.TotalPrice)
            .HasColumnType("jsonb");
    }

	private static void ConfigureTourPurchaseToken(ModelBuilder modelBuilder)
    {
		modelBuilder.Entity<User>().ToTable("Users", "stakeholders").Metadata.SetIsTableExcludedFromMigrations(true);

		modelBuilder.Entity<TourPurchaseToken>().HasKey(te => te.Id);

		modelBuilder.Entity<TourPurchaseToken>()
		   .HasOne<User>()
		   .WithMany()
		   .HasForeignKey(te => te.UserId);


		modelBuilder.Entity<TourPurchaseToken>()
			.HasOne<Tour>()
			.WithMany()
			.HasForeignKey(te => te.TourId);
	}
}
