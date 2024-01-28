using CodeSnippet.Domain.Aggregates;
using CodeSnippet.Domain.Entities;
using CodeSnippet.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CodeSnippet.Infrastructure.Data.Contexts;

public class SqlServerDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    public SqlServerDbContext(DbContextOptions<SqlServerDbContext> options, IConfiguration configuration) : base(options)
        => _configuration = configuration;
    
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        string connectionString = _configuration.GetConnectionString("SqlServerConnection") ?? throw new ArgumentNullException("SqlServerConnection is null");

        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}

