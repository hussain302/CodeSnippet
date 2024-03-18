using CodeSnippet.Domain.ValueObjects;

namespace CodeSnippet.Application.Dtos;

public record LoginResponseDto(Guid Id, string Username, string FirstName, string MiddleName, string LastName, string Email, RoleDto Role, Address Address, string accessToken);