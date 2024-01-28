using MediatR;
using CodeSnippet.Application.Dtos;
using CodeSnippet.Domain.Abstractions.Repositories;
using CodeSnippet.Application.Modules.Users.Querys;
using CodeSnippet.Application.Mappers;

namespace CodeSnippet.Application.Modules.Roles.Handlers;
public class UserQueryHandlers(IUserRepository userRepository) :
        IRequestHandler<GetUserByIdQuery, UserDto>,
        IRequestHandler<GetAllUsersQuery, List<UserDto>>
{
    private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));

    public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.Find(id: request.Id, cancellationToken);

        if (user is not null) 
            return new UserDto(
                user.Id, 
                user.Username, 
                user.FirstName,
                user.MiddleName,
                user.LastName,
                user.PasswordHash,
                user.Email,
                user.Role.AsDto(),
                user.Address ?? new Domain.ValueObjects.Address(
                                                    string.Empty,
                                                    string.Empty,
                                                    string.Empty,
                                                    string.Empty,
                                                    string.Empty),
                user.CreatedAt,
                user.RoleId,
                user.UpdatedAt);

        return new UserDto(
            Id: Guid.Empty, 
            Username: string.Empty,
            FirstName: string.Empty, 
            MiddleName: string.Empty,
            LastName: string.Empty,
            Password: string.Empty,
            Email: string.Empty,
            Role:new RoleDto(
                Guid.Empty,
                string.Empty,
                string.Empty
                ),
            Address: new Domain.ValueObjects.Address(
                string.Empty,
                string.Empty, 
                string.Empty, 
                string.Empty,
                string.Empty),
            CreatedAt: DateTime.MinValue,
            RoleId: Guid.Empty,
            UpdatedAt: DateTime.MinValue);
    }

    public async Task<List<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.Get(cancellationToken);

        if (users is not null) return users.Select(user=> user.AsDto()).ToList();

        return []; 
    }
}