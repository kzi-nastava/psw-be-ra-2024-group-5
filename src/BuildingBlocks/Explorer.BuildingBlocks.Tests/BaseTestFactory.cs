using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Explorer.API;

namespace Explorer.BuildingBlocks.Tests;

public abstract class BaseTestFactory : WebApplicationFactory<Program>
{
    protected abstract override void ConfigureWebHost(IWebHostBuilder builder);

    protected static void RunSetupScripts(DbContext context, string scriptFolder, ILogger logger)
    {
        try
        {
            var scriptFiles = Directory.GetFiles(scriptFolder);
            Array.Sort(scriptFiles);
            var script = string.Join('\n', scriptFiles.Select(File.ReadAllText));
            context.Database.ExecuteSqlRaw(script);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred seeding the database with test data. Error: {Message}", ex.Message);
        }
    }

    protected static void InitializeDatabases(ICollection<DbContext> contexts, string scriptsFolder, ILogger logger)
    {
        try
        {
            foreach (var context in contexts)
                context.Database.EnsureDeleted();

            foreach (var context in contexts)
            {
                context.Database.EnsureCreated();
                var databaseCreator = context.Database.GetService<IRelationalDatabaseCreator>();

                try { databaseCreator.CreateTables(); }
                catch (InvalidOperationException ex) when (ex.Message.Contains("already exists"))
                {
                    logger.LogWarning("Tables already exist in database: {DatabaseName}. Skipping table creation.",
                        context.Database.GetDbConnection().Database);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred creating the tables in database: {DatabaseName}. Error: {Message}",
                        context.Database.GetDbConnection().Database, ex.Message);
                }
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred initializing the databases. Error: {Message}", ex.Message);
        }
        finally
        {
            RunSetupScripts(contexts.First(), scriptsFolder, logger);
        }
    }

    protected ServiceProvider BuildServiceProvider(IServiceCollection services)
    {
        return ReplaceNeededDbContexts(services).BuildServiceProvider();
    }

    protected abstract IServiceCollection ReplaceNeededDbContexts(IServiceCollection services);

    protected static Action<DbContextOptionsBuilder> SetupTestContext()
    {
        var server = Environment.GetEnvironmentVariable("DATABASE_HOST") ?? "localhost";
        var port = Environment.GetEnvironmentVariable("DATABASE_PORT") ?? "5432";
        var database = Environment.GetEnvironmentVariable("DATABASE_SCHEMA") ?? "explorer-v1-test";
        var user = Environment.GetEnvironmentVariable("DATABASE_USERNAME") ?? "postgres";
        var password = Environment.GetEnvironmentVariable("DATABASE_PASSWORD") ?? "super";
        var integratedSecurity = Environment.GetEnvironmentVariable("DATABASE_INTEGRATED_SECURITY") ?? "false";
        var pooling = Environment.GetEnvironmentVariable("DATABASE_POOLING") ?? "true";

        var connectionString = $"Server={server};Port={port};Database={database};User ID={user};Password={password};Integrated Security={integratedSecurity};Pooling={pooling};Include Error Detail=True";

        return opt => opt.UseNpgsql(connectionString);
    }
}