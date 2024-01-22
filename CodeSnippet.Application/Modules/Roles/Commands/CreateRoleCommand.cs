using CodeSnippet.Application.Abstractions.MediatR;

namespace CodeSnippet.Application.Modules.Roles.Commands;

public record CreateRoleCommand(string Name, string Description) : ICommand<Guid>;