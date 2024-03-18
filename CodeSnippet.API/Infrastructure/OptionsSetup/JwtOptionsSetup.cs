using CodeSnippet.Infrastructure.Authentication;
using Microsoft.Extensions.Options;

namespace CodeSnippet.API.Infrastructure.OptionsSetup;

public class JwtOptionsSetup(IConfiguration configuration) : IConfigureOptions<JwtOptions>
{

    private const string SectioName = "Jwt";
    public void Configure(JwtOptions options)
    {
        configuration.GetSection(SectioName).Bind(options);
    }
}
