using CodeSnippet.Domain.Abstractions.Repositories;
using CodeSnippet.Domain.Aggregates;
using CodeSnippet.Infrastructure.Data.Contexts;

namespace CodeSnippet.Infrastructure.Repositories;

public class UserRepository(SqlServerDbContext context) : BaseRepository<User>(context), IUserRepository
{
}
