using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;
using CodeSnippet.Domain.Abstractions.Repositories;
using CodeSnippet.Infrastructure.Repositories;
using CodeSnippet.Infrastructure.Abstractions.Jwt;
using CodeSnippet.Infrastructure.Authentication;

namespace CodeSnippet.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        => services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly)
                   .AddScoped<IRoleRepository, RoleRepository>()
                   .AddScoped<IUserRepository, UserRepository>();
    
}
