using Explorer.Blog.Core.Domain;
using Microsoft.EntityFrameworkCore;
using BlogDomain = Explorer.Blog.Core.Domain.BlogPost;

namespace Explorer.Blog.Infrastructure.Database;

public class BlogContext : DbContext
{
    public BlogContext(DbContextOptions<BlogContext> options) : base(options) {}

    public DbSet<BlogDomain> blogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("blog");
        modelBuilder.Entity<BlogPost>().Property(blogPost => blogPost.votes).HasColumnType("jsonb");
        modelBuilder.Entity<BlogPost>().Property(blogPost => blogPost.images).HasColumnType("jsonb");
    }

    public DbSet<BlogComment> BlogComments { get; set; }
}