using CodeSnippet.Application.Abstractions.MediatR;
using CodeSnippet.Domain.Entities;
using CodeSnippet.Domain.ValueObjects;

namespace CodeSnippet.Application.Modules.Users.Commands;

public record UpdateUserCommand(Guid Id, string Username, string FirstName, string MiddleName, string LastName, string Password, string Email, Role Role, Address Address) : ICommand<bool>;