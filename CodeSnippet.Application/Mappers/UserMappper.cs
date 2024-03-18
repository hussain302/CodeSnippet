using CodeSnippet.Application.Dtos;
using CodeSnippet.Domain.Aggregates;
using CodeSnippet.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSnippet.Application.Mappers;
public static class UserMappper
{

    public static UserDto AsDto(this User source)
        => new
        (
            Id: source.Id,
            FirstName: source.FirstName,
            LastName: source.LastName,
            Email: source.Email,
            MiddleName: source.MiddleName,
            Address: source.Address,
            Password: source.PasswordHash,
            Role: source.Role?.AsDto() ?? RoleEmptyDto(),
            Username: source.Username,
            CreatedAt: source.CreatedAt,
            RoleId: source.RoleId,
            UpdatedAt: source.UpdatedAt
        );

    public static User AsDto(this UserDto source)
        => new()
        {
            Id = source.Id,
            FirstName = source.FirstName,
            LastName = source.LastName,
            Email = source.Email,
            MiddleName = source.MiddleName,
            Address = source.Address,
            PasswordHash = source.Password,
            Role = source.Role.AsEntity(),
            Username = source.Username,
            CreatedAt = source.CreatedAt,
            RoleId = source.RoleId,
            UpdatedAt = source.UpdatedAt
        };

    public static RoleDto RoleEmptyDto()
    {
        return new 
        (
            Id: Guid.Empty,
            Name: string.Empty,
            Description: string.Empty
        );
    }

    public static RegisterResponseDto AsRegisterResponseDto(this User source)
    => new 
    (
        source.Id,
        source.Username,
        source.FirstName,
        source.MiddleName,
        source.LastName,
        source.Email,
        source.Role.AsDto(),
        source.Address
    );

    public static LoginResponseDto AsLoginResponseDto(this User source, string accessToken)
    => new 
    (
        source.Id,
        source.Username,
        source.FirstName,
        source.MiddleName,
        source.LastName,
        source.Email,
        source.Role.AsDto(),
        source.Address,
        accessToken
    );
}
