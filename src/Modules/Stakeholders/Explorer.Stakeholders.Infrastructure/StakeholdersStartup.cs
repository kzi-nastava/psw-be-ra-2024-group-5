using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.API.Internal;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Mappers;
using Explorer.Stakeholders.Core.UseCases;
using Explorer.Stakeholders.Core.UseCases.Identity;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Stakeholders.Infrastructure.Database;
using Explorer.Stakeholders.Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Explorer.Stakeholders.Core.Domain.Messages;

namespace Explorer.Stakeholders.Infrastructure;

public static class StakeholdersStartup
{
    public static IServiceCollection ConfigureStakeholdersModule(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(StakeholderProfile).Assembly);
        SetupCore(services);
        SetupInfrastructure(services);
        return services;
    }
    
    private static void SetupCore(IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<ITokenGenerator, JwtGenerator>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IUserProfileService, UserProfileService>();
        services.AddScoped<IAppRatingService, AppRatingService>();
        services.AddScoped<IClubService, ClubService>();
        services.AddScoped<IUserService, InternalUserService>();
        services.AddScoped<IFollowingService, FollowingService>();
        services.AddScoped<IProfileMessagesService, ProfileMessagesService>();
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<IUserProfileServiceInternal, UserProfileServiceInternal>();
    }

    private static void SetupInfrastructure(IServiceCollection services)
    {
        services.AddScoped(typeof(ICrudRepository<Person>), typeof(CrudDatabaseRepository<Person, StakeholdersContext>));
        services.AddScoped(typeof(ICrudRepository<User>), typeof(CrudDatabaseRepository<User, StakeholdersContext>));
        services.AddScoped<IUserRepository, UserDatabaseRepository>();
        services.AddScoped(typeof(ICrudRepository<AppRating>), typeof(CrudDatabaseRepository<AppRating, StakeholdersContext>));
        services.AddScoped<IAppRatingRepository, AppRatingDatabaseRepository>();
        services.AddScoped<IUserProfileRepository, UserProfileDatabaseRepository>();
        services.AddScoped(typeof(ICrudRepository<Club>), typeof(CrudDatabaseRepository<Club, StakeholdersContext>));
        services.AddScoped(typeof(ICrudRepository<Following>), typeof(CrudDatabaseRepository<Following, StakeholdersContext>));
        services.AddScoped<IClubRepository, ClubRepository>();
        services.AddScoped<INotificationRepository, NotificationRepository>();
        services.AddScoped(typeof(ICrudRepository<ClubMessage>), typeof(CrudDatabaseRepository<ClubMessage, StakeholdersContext>));
        services.AddScoped<IPersonRepository, PersonRepository>();

        services.AddDbContext<StakeholdersContext>(opt =>
            opt.UseNpgsql(DbConnectionStringBuilder.Build("stakeholders"),
                x => x.MigrationsHistoryTable("__EFMigrationsHistory", "stakeholders")));     

    }
}