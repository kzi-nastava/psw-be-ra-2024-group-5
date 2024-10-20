using Explorer.Tours.Core.Domain;
using Explorer.Stakeholders.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Tours.Infrastructure.Database;

public class ToursContext : DbContext
{
    public DbSet<Tour> Tours { get; set; }
    public DbSet<Equipment> Equipment { get; set; }
    public DbSet<TourEquipment> TourEquipment { get; set; }
    public DbSet<KeyPoint> KeyPoint { get; set; }
    public DbSet<Facility> Facilities { get; set; }
    public DbSet<Preference> Preferences { get; set; }

    public ToursContext(DbContextOptions<ToursContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("tours");

        modelBuilder.Entity<Tour>().HasKey(t => t.Id);
        //modelBuilder.Entity<Tour>().HasIndex(t => t.Id).IsUnique();

        modelBuilder.Entity<Equipment>().HasKey(e => e.Id);
        
        modelBuilder.Entity<TourEquipment>().HasKey(te => new { te.TourId, te.EquipmentId });
        
        modelBuilder.Entity<TourEquipment>()
            .HasOne<Tour>()
            .WithMany()
            .HasForeignKey(te => te.TourId);
        
        modelBuilder.Entity<TourEquipment>()
            .HasOne<Equipment>()
            .WithMany()
            .HasForeignKey(te => te.EquipmentId);
        
        modelBuilder.Entity<KeyPoint>().HasIndex(k => k.Id).IsUnique();
        ConfigureTour(modelBuilder);
        ConfigurePreference(modelBuilder);
    }

    private static void ConfigureTour(ModelBuilder modelBuilder) {
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
        modelBuilder.Entity<Preference>()
            .HasKey(p => p.Id);

        // Konfigurišemo entitet Preference da koristi tabelu Users iz šeme stakeholders
        modelBuilder.Entity<Preference>()
            .HasOne<User>() // Definišemo vezu prema entitetu User
            .WithMany() // Definišemo da User može imati više Preference
            .HasForeignKey(p => p.TouristId) // Definišemo stranu koja sadrži ključ
            .OnDelete(DeleteBehavior.Cascade) // Postavljamo ponašanje pri brisanju
            .HasConstraintName("FK_Preferences_User_TouristId") // Ime strane
            .HasPrincipalKey(u => u.Id); // Glavni ključ za User

        // Ignorišemo kreiranje tabele za User entitet
        modelBuilder.Ignore<User>();
    }


}

//﻿using Explorer.Tours.Core.Domain;
//using Microsoft.EntityFrameworkCore;

//namespace Explorer.Tours.Infrastructure.Database;

//public class ToursContext : DbContext {
//    public DbSet<Equipment> Equipment { get; set; }

//    public ToursContext(DbContextOptions<ToursContext> options) : base(options) { }

//    protected override void OnModelCreating(ModelBuilder modelBuilder) {
//        modelBuilder.HasDefaultSchema("tours");
//    }
//}
