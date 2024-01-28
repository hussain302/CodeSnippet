using CodeSnippet.Application.Abstractions.MediatR;

namespace CodeSnippet.Application.Modules.Roles.Commands;
public record DeleteRoleCommand(Guid Id) : ICommand<bool>;
