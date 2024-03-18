
using CodeSnippet.Domain.ValueObjects;

namespace CodeSnippet.Application.Dtos;

public record RegisterResponseDto(Guid Id, string Username, string FirstName, string MiddleName, string LastName, string Email, RoleDto Role, Address Address);