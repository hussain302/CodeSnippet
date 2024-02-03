using CodeSnippet.Application.Dtos;
using CodeSnippet.Domain.Entities;

namespace CodeSnippet.Application.Mappers;
public static class RoleMapper
{

    public static Role AsEntity(this RoleDto source)
        => new()
        {
            Id = source.Id,
            Name = source.Name,
            Description = source.Description,
        };

    public static RoleDto AsDto(this Role source)
        => new
        (
            Id: source.Id,
            Name: source.Name,
            Description: source.Description
        );

}
