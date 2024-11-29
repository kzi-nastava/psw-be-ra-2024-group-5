using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Payments.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Payments.Infrastructure.Database
{
    public class PaymentsContext : DbContext
    {
        public PaymentsContext(DbContextOptions<PaymentsContext> options) : base(options) { }

        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<TourPurchaseToken> TourPurchaseTokens { get; set; }
        public DbSet<Bundle> Bundles { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
		public DbSet<Coupon> Coupons { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("payments");

            ConfigureShoppingCarts(modelBuilder);
            ConfigureTourPurchaseToken(modelBuilder);
            ConfigureBundle(modelBuilder);
            ConfigureWallet(modelBuilder);
			ConfigureCoupon(modelBuilder); 

		}

		private static void ConfigureShoppingCarts(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShoppingCart>()
                .HasKey(sc => sc.Id);

            modelBuilder.Entity<ShoppingCart>()
                .HasMany(sc => sc.Items)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            /*
            modelBuilder.Entity<OrderItem>()
                .HasOne<Tour>()
                .WithMany()
                .HasForeignKey(oi => oi.TourId);*/

            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.Price)
                .HasColumnType("jsonb");

            modelBuilder.Entity<ShoppingCart>()
                .Property(sc => sc.TotalPrice)
                .HasColumnType("jsonb");
        }

        private static void ConfigureTourPurchaseToken(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TourPurchaseToken>().HasKey(te => te.Id);
        }

        private static void ConfigureBundle(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bundle>()
                 .Property(b => b.Price)
                 .HasColumnType("jsonb");

            modelBuilder.Entity<Bundle>()
                .Property(b => b.BundleItems)
                .HasColumnType("jsonb");
        }
        private static void ConfigureWallet(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Wallet>().HasKey(te => te.Id);

            modelBuilder.Entity<Wallet>()
                .Property(oi => oi.Balance)
                .HasColumnType("jsonb");
        }
		private static void ConfigureCoupon(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Coupon>()
				.Property(c => c.ExpiredDate)
				.HasConversion(
					v => v.ToUniversalTime(),
					v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
				);
		}
	}
}
