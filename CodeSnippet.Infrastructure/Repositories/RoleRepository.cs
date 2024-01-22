using CodeSnippet.Domain.Abstractions.Repositories;
using CodeSnippet.Domain.Entities;
using CodeSnippet.Infrastructure.Data.Contexts;

namespace CodeSnippet.Infrastructure.Repositories;

public class RoleRepository(SqlServerDbContext context) : BaseRepository<Role>(context), IRoleRepository
{
}
