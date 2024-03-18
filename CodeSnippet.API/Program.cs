using CodeSnippet.Infrastructure;
using CodeSnippet.Application;
using CodeSnippet.API.Middleware;
using CodeSnippet.API.Infrastructure.OptionsSetup;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
builder.Services.AddControllers();

builder.Services.ConfigureOptions<JwtOptionsSetup>();

builder.Services.AddInfrastructure(configuration);

builder.Services.AddApplication(configuration);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

WebApplication? app = builder.Build();

app.UseGlobalExceptionHandler();

app.UseApiVersioning();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Code Snippet APIs");
});

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
