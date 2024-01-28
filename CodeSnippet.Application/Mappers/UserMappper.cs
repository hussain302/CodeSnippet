using CodeSnippet.Application.Dtos;
using CodeSnippet.Domain.Aggregates;
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
            Role: source.Role.AsDto(),
            Username: source.Username,
            CreatedAt: source.CreatedAt,
            RoleId: source.RoleId,
            UpdatedAt:source.UpdatedAt
        );  
    
    public static User AsDto(this UserDto source)
        => new () 
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

}
