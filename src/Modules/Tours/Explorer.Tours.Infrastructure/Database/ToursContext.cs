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

        ConfigureTour(modelBuilder);
    }

    private static void ConfigureTour(ModelBuilder modelBuilder) {
        modelBuilder.Entity<KeyPoint>()
            .HasOne<Tour>()
            .WithMany() 
            .HasForeignKey(k => k.TourId)
            .OnDelete(DeleteBehavior.Cascade);
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