
using CodeSnippet.Application.Abstractions.MediatR;
using CodeSnippet.Application.Dtos;

namespace CodeSnippet.Application.Modules.Auth.Queries;
public record GetLoginUserQuery(string? Username, string? Email, string Password) : IQuery<LoginResponseDto>;
