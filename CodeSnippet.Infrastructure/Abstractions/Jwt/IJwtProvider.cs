
using CodeSnippet.Domain.Aggregates;

namespace CodeSnippet.Infrastructure.Abstractions.Jwt;
public interface IJwtProvider
{
    string GenerateToken(User user);
}
