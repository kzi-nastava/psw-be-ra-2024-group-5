using Explorer.Blog.Infrastructure.Database;
using Explorer.BuildingBlocks.Tests;
using Explorer.Stakeholders.Infrastructure.Database;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Explorer.Tours.Tests;

public class ToursTestFactory : BaseTestFactory
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

            var logger = scopedServices.GetRequiredService<ILogger<BaseTestFactory>>();

            var path = Path.Combine(".", "..", "..", "..", "TestData");

            RunSetupScripts(stakeholdersDb, path, logger);
            InitializeDatabases(new List<DbContext> { stakeholdersDb, blogsDb, toursDb }, path, logger);
        });
    }

    protected override IServiceCollection ReplaceNeededDbContexts(IServiceCollection services)
    {
        var stakeholderDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<StakeholdersContext>));
        var blogDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<BlogContext>));
        var toursDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ToursContext>));

        services.Remove(stakeholderDescriptor!);
        services.Remove(blogDescriptor!);
        services.Remove(toursDescriptor!);

        services.AddDbContext<StakeholdersContext>(SetupTestContext());
        services.AddDbContext<BlogContext>(SetupTestContext());
        services.AddDbContext<ToursContext>(SetupTestContext());

        return services;
    }
}
