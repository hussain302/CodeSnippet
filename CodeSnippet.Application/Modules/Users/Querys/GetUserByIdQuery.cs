using CodeSnippet.Application.Dtos;
using CodeSnippet.Application.Abstractions.MediatR;

namespace CodeSnippet.Application.Modules.Users.Querys;
public record GetUserByIdQuery(Guid Id) : IQuery<UserDto>;
