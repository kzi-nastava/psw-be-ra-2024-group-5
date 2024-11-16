using Explorer.Encounters.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Infrastructure.Database;
public class EncountersContext : DbContext 
{
    public DbSet<Encounter> Encounters { get; set; }

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
    }
}
