using Explorer.Stakeholders.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Stakeholders.Infrastructure.Database;

public class StakeholdersContext : DbContext
{  
    public DbSet<User> Users { get; set; }
    public DbSet<Person> People { get; set; }
    public DbSet<UserProfile> Profiles { get; set; }
    public DbSet<Club> Clubs { get; set; }

    public DbSet<AppRating> AppRating { get; set; }

    public DbSet<ClubMembership> Memberships { get; set; }
    public DbSet<Follower> Followers { get; set; }

    public StakeholdersContext(DbContextOptions<StakeholdersContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("stakeholders");

        modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();

        ConfigureStakeholder(modelBuilder);
    }

    private static void ConfigureStakeholder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>()
            .HasOne<User>()
            .WithOne()
            .HasForeignKey<Person>(s => s.UserId);

        modelBuilder.Entity<UserProfile>()
            .HasOne<User>()
            .WithOne()
            .HasForeignKey<UserProfile>(s => s.UserId);

        modelBuilder.Entity<ClubMembership>()
        .ToTable("Memberships", "stakeholders")
        .HasKey(cm => new { cm.UserId, cm.ClubId });

        modelBuilder.Entity<ClubMembership>()
            .HasOne<Club>()
            .WithMany()
            .HasForeignKey(cm => cm.ClubId);

        modelBuilder.Entity<ClubMembership>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(cm => cm.UserId);

        modelBuilder.Entity<Follower>()
        .HasOne<User>()
        .WithMany()   
        .HasForeignKey(f => f.UserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Follower>()
            .HasOne<User>()
            .WithMany()   
            .HasForeignKey(f => f.FollowedUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}