using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Preferences.Core.Domain.RepositoryInterfaces;
using Explorer.Preferences.Infrastructure.Database.Repositories;
using Explorer.Tours.API.Internal;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.Author;
using Explorer.Tours.API.Public.Tourist;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Mappers;
using Explorer.Tours.Core.UseCases;
using Explorer.Tours.Core.UseCases.Administration;
using Explorer.Tours.Core.UseCases.Author;
using Explorer.Tours.Core.UseCases.Tourist;
using Explorer.Tours.Infrastructure.Database;
using Explorer.Tours.Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Explorer.Tours.Infrastructure;

public static class ToursStartup
{
    public static IServiceCollection ConfigureToursModule(this IServiceCollection services)
    {
        // Registers all profiles since it works on the assembly
        services.AddAutoMapper(typeof(ToursProfile).Assembly);
        SetupCore(services);
        SetupInfrastructure(services);
        return services;
    }
    
    private static void SetupCore(IServiceCollection services)
    {
        services.AddScoped<ITourService, TourService>();
        services.AddScoped<IInternalTourService, InternalTourService>();
        services.AddScoped<ITouristService, TouristService>();
        services.AddScoped<IEquipmentService, EquipmentService>();
        services.AddScoped<IKeyPointService, KeyPointService>();
        services.AddScoped<IFacilityService, FacilityService>();
        services.AddScoped<IPreferenceService, PreferenceService>();
        services.AddScoped<ITourReviewService, TourReviewService>();
        services.AddScoped<ITourExecutionService, TourExecutionService>();
    }

	private static void SetupInfrastructure(IServiceCollection services)
    {
        services.AddScoped(typeof(ICrudRepository<Tour>), typeof(CrudDatabaseRepository<Tour, ToursContext>));
        services.AddScoped(typeof(ICrudRepository<Equipment>), typeof(CrudDatabaseRepository<Equipment, ToursContext>));
        services.AddScoped<IEquipmentRepository, EquipmentRepository>();
        services.AddScoped<ITourRepository, TourRepository>();
        services.AddScoped(typeof(ICrudRepository<KeyPoint>), typeof(CrudDatabaseRepository<KeyPoint, ToursContext>));
        services.AddScoped(typeof(ICrudRepository<Facility>), typeof(CrudDatabaseRepository<Facility, ToursContext>));
        services.AddScoped(typeof(ICrudRepository<Preference>), typeof(CrudDatabaseRepository<Preference, ToursContext>));
		services.AddScoped<ITouristRepository, TouristRepository>();
        services.AddScoped<ITourExecutionRepository, TourExecutionRepository>();
        services.AddScoped<ITourReviewRepository, TourReviewDatabaseRepository>();
        services.AddScoped<ICrudRepository<TourReview>>(sp => sp.GetRequiredService<ITourReviewRepository>());
        services.AddScoped<IPreferenceRepository, PreferenceRepository>();


        services.AddDbContext<ToursContext>(opt =>
            opt.UseNpgsql(DbConnectionStringBuilder.Build("tours"),
                x => x.MigrationsHistoryTable("__EFMigrationsHistory", "tours")));
    }
}