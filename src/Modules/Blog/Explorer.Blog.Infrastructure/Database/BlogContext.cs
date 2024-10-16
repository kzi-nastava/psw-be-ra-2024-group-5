using Explorer.Blog.Core.Domain;
using Microsoft.EntityFrameworkCore;
using BlogDomain = Explorer.Blog.Core.Domain.Blog;

namespace Explorer.Blog.Infrastructure.Database;

public class BlogContext : DbContext
{
    public BlogContext(DbContextOptions<BlogContext> options) : base(options) {}

    public DbSet<BlogDomain> blogs { get; set; }
    public DbSet<BlogImage> blogImages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("blog");

        modelBuilder.Entity<BlogDomain>()
            .HasMany(b => b.images)
            .WithOne()
            .HasForeignKey("blogId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}