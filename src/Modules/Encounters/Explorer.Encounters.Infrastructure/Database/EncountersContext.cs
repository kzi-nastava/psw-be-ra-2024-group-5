using Explorer.Encounters.API.Enum;
using Explorer.Encounters.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Encounters.Core;
using Explorer.Stakeholders.Core.Domain;

namespace Explorer.Encounters.Infrastructure.Database;
public class EncountersContext : DbContext 
{
    public DbSet<Encounter> Encounters { get; set; }
    public DbSet<SocialEncounter> SocialEncounters { get; set; }
    public DbSet<EncounterExecution> EncountersExecution { get; set; }
    public DbSet<Participant> Participants { get; set; }

    public EncountersContext(DbContextOptions<EncountersContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("encounters");

        modelBuilder
            .Entity<Encounter>()
            .HasKey(e => e.Id);

        modelBuilder.Entity<Encounter>()
            .Property(e => e.Location)
            .HasColumnType("jsonb");

        modelBuilder.Entity<SocialEncounter>()
            .ToTable("SocialEncounters");

        modelBuilder.Entity<SocialEncounter>()
            .Property(se => se.UserIds)
            .HasColumnType("jsonb");

        ConfigureParticipant(modelBuilder);
    }

    private static void ConfigureParticipant(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Participant>()
            .HasKey(p => p.Id);

        modelBuilder.Entity<User>()
           .ToTable("Users", "stakeholders")
           .Metadata.SetIsTableExcludedFromMigrations(true);

        modelBuilder.Entity<Participant>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
