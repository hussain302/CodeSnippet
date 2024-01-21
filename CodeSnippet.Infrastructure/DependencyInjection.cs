using CodeSnippet.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeSnippet.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

        string connectionString = configuration.GetConnectionString("SqlServerConnection") 
            ?? throw new ArgumentNullException("Connection String can't be null");
        
        services.AddDistributedMemoryCache();

        services.AddScoped<SqlServerDbContext>(); //(options => options.UseSqlServer(connectionString));
        
    }
}
