using Explorer.Blog.Infrastructure.Database;
using Explorer.BuildingBlocks.Tests;
using Explorer.Payments.Infrastructure.Database;
using Explorer.Stakeholders.Infrastructure.Database;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Explorer.Blog.Tests;

public class BlogTestFactory : BaseTestFactory
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            using var scope = BuildServiceProvider(services).CreateScope();
            var scopedServices = scope.ServiceProvider;

            var stakeholdersDb = scopedServices.GetRequiredService<StakeholdersContext>();
            var blogsDb = scopedServices.GetRequiredService<BlogContext>();
            var toursDb = scopedServices.GetRequiredService<ToursContext>();
            var paymentsDb = scopedServices.GetRequiredService<PaymentsContext>();

            var logger = scopedServices.GetRequiredService<ILogger<BaseTestFactory>>();

            var path = Path.Combine(".", "..", "..", "..", "TestData");

            InitializeDatabases(new List<DbContext> { stakeholdersDb, blogsDb, toursDb, paymentsDb }, path, logger);
        });
    }

    protected override IServiceCollection ReplaceNeededDbContexts(IServiceCollection services)
    {
        var stakeholderDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<StakeholdersContext>));
        var blogDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<BlogContext>));
        var toursDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ToursContext>));
        var paymentsDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<PaymentsContext>));


        services.Remove(stakeholderDescriptor!);
        services.Remove(blogDescriptor!);
        services.Remove(toursDescriptor!);
        services.Remove(paymentsDescriptor!);

        services.AddDbContext<StakeholdersContext>(SetupTestContext());
        services.AddDbContext<BlogContext>(SetupTestContext());
        services.AddDbContext<ToursContext>(SetupTestContext());
        services.AddDbContext<PaymentsContext>(SetupTestContext());

        return services;
    }
}
