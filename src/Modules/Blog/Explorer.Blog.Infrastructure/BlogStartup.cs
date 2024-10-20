using Explorer.Blog.API.Public;
using Explorer.Blog.Core.Domain;
using Explorer.Blog.Core.Domain.RepositoryInterfaces;
using Explorer.Blog.Core.Mappers;
using Explorer.Blog.Core.UseCases;
using Explorer.Blog.Infrastructure.Database;
using Explorer.Blog.Infrastructure.Database.Repositories;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using domainBlog = Explorer.Blog.Core.Domain.Blog;

namespace Explorer.Blog.Infrastructure
{
    public static class BlogStartup
    {
        services.AddScoped<IBlogService, BlogService>();
        services.AddScoped<IClubService, ClubService>();
    }

    private static void SetupInfrastructure(IServiceCollection services)
    {
        services.AddScoped(typeof(ICrudRepository<domainBlog>), typeof(CrudDatabaseRepository<domainBlog, BlogContext>));
        services.AddScoped(typeof(ICrudRepository<BlogImage>), typeof(CrudDatabaseRepository<BlogImage, BlogContext>));
        services.AddScoped(typeof(ICrudRepository<Club>), typeof(CrudDatabaseRepository<Club, BlogContext>));

        public static IServiceCollection ConfigureBlogModule(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(BlogProfile).Assembly);
            SetupCore(services);
            SetupInfrastructure(services);
            return services;
        }

        private static void SetupCore(IServiceCollection services)
        {
            services.AddScoped<IClubService, ClubService>();
            services.AddScoped<IBlogCommentService, BlogCommentService>();
            services.AddScoped<ICommentRepository, CommentRepository>();
        }

        private static void SetupInfrastructure(IServiceCollection services)
        {
            services.AddScoped(typeof(ICrudRepository<Club>), typeof(CrudDatabaseRepository<Club, BlogContext>));
            services.AddScoped(typeof(ICrudRepository<BlogComment>), typeof(CrudDatabaseRepository<BlogComment, BlogContext>));

            services.AddDbContext<BlogContext>(opt =>
                opt.UseNpgsql(DbConnectionStringBuilder.Build("blog"),
                    x => x.MigrationsHistoryTable("__EFMigrationsHistory", "blog")));
        }
    }
}
