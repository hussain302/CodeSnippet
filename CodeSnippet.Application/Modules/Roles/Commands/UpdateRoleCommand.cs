using MediatR;
using CodeSnippet.Application.Abstractions.MediatR;

namespace CodeSnippet.Application.Modules.Roles.Commands;
public record UpdateRoleCommand(Guid Id, string Name, string Description) : ICommand<bool>;
