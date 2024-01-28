using CodeSnippet.Infrastructure;
using CodeSnippet.Application;
using CodeSnippet.API.Middleware;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers();

builder.Services.AddInfrastructure(configuration);

builder.Services.AddApplication(configuration);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseGlobalExceptionHandler();

app.UseApiVersioning();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Code Snippet APIs");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
