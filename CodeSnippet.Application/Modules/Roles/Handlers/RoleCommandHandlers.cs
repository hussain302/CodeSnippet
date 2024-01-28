using MediatR;
using CodeSnippet.Domain.Entities;
using CodeSnippet.Application.Modules.Roles.Commands;
using CodeSnippet.Domain.Abstractions.Repositories;

namespace CodeSnippet.Application.Modules.Roles.Handlers;
public class RoleCommandHandlers(IRoleRepository roleRepository) :
        IRequestHandler<CreateRoleCommand, Guid>,
        IRequestHandler<UpdateRoleCommand, bool>,
        IRequestHandler<DeleteRoleCommand, bool>
{
    private readonly IRoleRepository _roleRepository = roleRepository;

    public async Task<Guid> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        Role role = new ()
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description
        };

        var existingRole = await _roleRepository.Get(role => role.Name == request.Name, cancellationToken:cancellationToken);

        if (existingRole.Any()) throw new Exception("Role name already exists.");

        await _roleRepository.Add(role, cancellationToken);

        int saveResponse = await _roleRepository.Save(cancellationToken);

        if (saveResponse is 0) return Guid.Empty;

        return role.Id;
    }

    public async Task<bool> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        Role? role = await _roleRepository.Find(id: request.Id, cancellationToken);

        if (role == null) return false;
        
        role.Name = request.Name;

        role.Description = request.Description;

        await _roleRepository.Update(role, cancellationToken);

        int saveResponse = await _roleRepository.Save(cancellationToken);

        if(saveResponse is 0) return false;

        return true;
    }

    public async Task<bool> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        Role? role = await _roleRepository.Find(id: request.Id, cancellationToken);

        if (role != null) await _roleRepository.Delete(role, cancellationToken);

        int saveResponse = await _roleRepository.Save(cancellationToken);

        if (saveResponse is 0) return false;

        return true;
    }
}