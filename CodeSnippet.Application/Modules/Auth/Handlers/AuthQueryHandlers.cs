using MediatR;
using CodeSnippet.Application.Dtos;
using CodeSnippet.Domain.Aggregates;
using CodeSnippet.Application.Mappers;
using CodeSnippet.Application.Modules.Auth.Queries;
using CodeSnippet.Domain.Abstractions.Repositories;
using CodeSnippet.Infrastructure.Abstractions.Jwt;

namespace CodeSnippet.Application.Modules.Auth.Handlers;

public class AuthQueryHandlers(IUserRepository userRepository, IJwtProvider jwtProvider) :
        IRequestHandler<GetLoginUserQuery, LoginResponseDto>
{
    private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));

    public async Task<LoginResponseDto> Handle(GetLoginUserQuery request, CancellationToken cancellationToken)
    {
        string token = string.Empty;
        var users = await _userRepository.Get(includes: x => x.Role, cancellationToken: cancellationToken);

        User user = users.FirstOrDefault(x => (x.Email == request.Email
                                                 || x.Username == request.Username)
                                                 && x.PasswordHash == request.Password)
                                                 ?? new User { };

        if (user != null)  return user.AsLoginResponseDto(jwtProvider.GenerateToken(user));        

        return null;
    } 
}