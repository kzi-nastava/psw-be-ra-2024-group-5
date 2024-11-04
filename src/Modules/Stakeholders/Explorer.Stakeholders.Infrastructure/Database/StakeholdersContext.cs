using Explorer.Stakeholders.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Stakeholders.Infrastructure.Database;

public class StakeholdersContext : DbContext
{  
    public DbSet<User> Users { get; set; }
    public DbSet<Person> People { get; set; }
    public DbSet<UserProfile> Profiles { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Club> Clubs { get; set; }

    public DbSet<AppRating> AppRating { get; set; }

    public DbSet<ClubMembership> Memberships { get; set; }
    public DbSet<Following> Followers { get; set; }

    public StakeholdersContext(DbContextOptions<StakeholdersContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("stakeholders");

        modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();
        modelBuilder.Entity<UserProfile>().HasKey(u => u.Id);
        modelBuilder.Entity<Message>().HasKey(m => m.Id);

        ConfigureStakeholder(modelBuilder);
    }

    private static void ConfigureStakeholder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>()
            .HasOne<User>()
            .WithOne()
            .HasForeignKey<Person>(s => s.UserId);

        //modelBuilder.Entity<Message>()
        //    .HasOne<UserProfile>()
        //    .WithMany()
        //    .HasForeignKey(m => m.SenderId);


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

        modelBuilder.Entity<Following>()
            .HasOne<User>()
            .WithMany()   
            .HasForeignKey(f => f.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Following>()
            .HasOne<User>()
            .WithMany()   
            .HasForeignKey(f => f.FollowedUserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<UserProfile>()
            .HasOne<User>()
            .WithOne()
            .HasForeignKey<UserProfile>(s => s.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<UserProfile>()
            .HasMany(p => p.Messages)
            .WithOne()
            .HasForeignKey(m => m.RecipientId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Message>()
            .ToTable("Messages", "stakeholders")
            .Property(m => m.Attachment).HasColumnType("jsonb");
    }
}