using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Payments.API.Internal;
using Explorer.Payments.API.Public.Tourist;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Payments.Core.Mappers;
using Explorer.Payments.Core.UseCases;
using Explorer.Payments.Infrastructure.Database;
using Explorer.Payments.Infrastructure.Database.Repositories;
using Explorer.Tours.Core.UseCases.Tourist;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Infrastructure
{
    public static class PaymentsStartup
    {
        public static IServiceCollection ConfigurePaymentsModule(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(PaymentsProfile).Assembly);
            SetupCore(services);
            SetupInfrastructure(services);
            return services;
        }

        private static void SetupCore(IServiceCollection services)
        {
            services.AddScoped<IShoppingCartService, ShoppingCartService>();
            services.AddScoped<IInternalShoppingCartService, InternalShoppingCartService>();
            services.AddScoped<IBundleService, BundleService>();

            services.AddScoped<IInternalWalletService, WalletService>();
            services.AddScoped<IWalletService, WalletService>();
        }

        private static void SetupInfrastructure(IServiceCollection services)
        {
            services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
            services.AddScoped<IBundleRepository, BundleRepository>();
            services.AddScoped<IWalletRepository, WalletRepository>();

            services.AddDbContext<PaymentsContext>(opt =>
            opt.UseNpgsql(DbConnectionStringBuilder.Build("payments"),
                x => x.MigrationsHistoryTable("__EFMigrationsHistory", "payments")));
        }
    }
}
