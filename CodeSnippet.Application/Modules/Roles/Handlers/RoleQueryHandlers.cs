﻿using MediatR;
using CodeSnippet.Application.Dtos;
using CodeSnippet.Application.Modules.Roles.Querys;
using CodeSnippet.Domain.Abstractions.Repositories;
using CodeSnippet.Application.Mappers;

namespace CodeSnippet.Application.Modules.Roles.Handlers;
public class RoleQueryHandlers(IRoleRepository roleRepository) :
        IRequestHandler<GetRoleByIdQuery, RoleDto>,
        IRequestHandler<GetAllRolesQuery, List<RoleDto>>
{
    private readonly IRoleRepository _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));

    public async Task<RoleDto> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.Find(id: request.Id, cancellationToken);

        if (role is not null) return new RoleDto(role.Id, role.Name, role.Description);
        
        return new RoleDto(Id: Guid.Empty,Name: string.Empty,Description: string.Empty);
    }

    public async Task<List<RoleDto>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {

        var roles = await _roleRepository.Get(cancellationToken);

        if (roles is not null) return roles.Select(role => role.AsDto()).ToList();
        
        return []; 
    }
}