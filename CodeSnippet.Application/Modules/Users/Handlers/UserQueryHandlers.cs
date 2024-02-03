using MediatR;
using CodeSnippet.Application.Dtos;
using CodeSnippet.Domain.Abstractions.Repositories;
using CodeSnippet.Application.Modules.Users.Querys;
using CodeSnippet.Application.Mappers;
using CodeSnippet.Domain.Aggregates;

namespace CodeSnippet.Application.Modules.Roles.Handlers;
public class UserQueryHandlers(IUserRepository userRepository) :
        IRequestHandler<GetUserByIdQuery, UserDto>,
        IRequestHandler<GetAllUsersQuery, List<UserDto>>
{
    private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));

    public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.Get(includes:x=>x.Role, cancellationToken: cancellationToken);

        User user = users.SingleOrDefault(x=>x.Id == request.Id) ?? new User { };
        
        return user.AsDto();
    }

    public async Task<List<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.Get(includes:x=>x.Role, cancellationToken: cancellationToken);

        if (users is not null) return users.Select(user=> user.AsDto()).ToList();

        return []; 
    }
}