using System.Text;
using System.Security.Claims;
using CodeSnippet.Domain.Aggregates;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using CodeSnippet.Infrastructure.Abstractions.Jwt;
using Microsoft.Extensions.Options;

namespace CodeSnippet.Infrastructure.Authentication;
public sealed class JwtProvider(IOptions<JwtOptions> jwtOptions) : IJwtProvider
{
    private readonly JwtOptions jwtOptions = jwtOptions.Value;

    public string GenerateToken(User user)
    {
        Claim[]? claims =
        [
            new (JwtRegisteredClaimNames.NameId, user.Username),
            new (JwtRegisteredClaimNames.Email, user.Email),
            new (JwtRegisteredClaimNames.Name, $"{user.FirstName}, {user.LastName}")
        ];

        SigningCredentials sigingCredentials = new (
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtOptions.SecurityKey)), 
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(jwtOptions.Issuer,
            jwtOptions.Audience,
              claims,
              expires: DateTime.Now.AddMinutes(90),
              signingCredentials: sigingCredentials);

        string tokenValue = new JwtSecurityTokenHandler()
            .WriteToken(token);

        return tokenValue;
    }
}
