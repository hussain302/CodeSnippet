using CodeSnippet.Domain.ValueObjects;

namespace CodeSnippet.Application.Dtos;
public record UserDto(Guid Id, string Username, string FirstName, string MiddleName, string LastName, string Password, string Email, RoleDto Role, Address Address, DateTime CreatedAt, Guid RoleId, DateTime UpdatedAt);
