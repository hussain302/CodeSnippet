using CodeSnippet.Domain.Aggregates;
using CodeSnippet.Domain.Entities;
using CodeSnippet.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace CodeSnippet.Infrastructure.Data.Contexts;

public class SqlServerDbContext : DbContext
{
    public SqlServerDbContext(DbContextOptions<SqlServerDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
