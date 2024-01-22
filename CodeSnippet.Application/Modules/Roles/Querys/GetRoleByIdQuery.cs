using CodeSnippet.Application.Dtos;
using CodeSnippet.Application.Abstractions.MediatR;

namespace CodeSnippet.Application.Modules.Roles.Querys;
public record GetRoleByIdQuery(Guid Id) : IQuery<RoleDto>;
