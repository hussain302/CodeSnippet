using CodeSnippet.Application.Abstractions.MediatR;

namespace CodeSnippet.Application.Modules.Users.Commands;
public record DeleteUserCommand(Guid Id) : ICommand<bool>;
