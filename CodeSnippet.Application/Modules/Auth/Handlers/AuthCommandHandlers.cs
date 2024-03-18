using CodeSnippet.Application.Modules.Auth.Commands;
using CodeSnippet.Domain.Abstractions.Repositories;
using CodeSnippet.Domain.Aggregates;
using MediatR;

namespace CodeSnippet.Application.Modules.Auth.Handlers;

public class AuthCommandHandlers(IUserRepository userRepository) :
    IRequestHandler<CreateRegisterCommand, Guid>
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<Guid> Handle(CreateRegisterCommand request, CancellationToken cancellationToken)
    {
        User? user = new()
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName,
            MiddleName = request.MiddleName,
            LastName = request.LastName,
            Email = request.Email,
            Username = request.Username,
            RoleId = request.Role.Id,
            CreatedAt = DateTime.Now,
            Address = request.Address,
            PasswordHash = request.Password,
            UpdatedAt = DateTime.Now
        };

        var existingUser = await _userRepository.Get(
            x => x.Username == request.Username || x.Email == request.Email,
            cancellationToken: cancellationToken
        );

        if (existingUser.Any()) throw new Exception("Username or Email already exists. Please choose unique one");

        await _userRepository.Add(user, cancellationToken);

        int saveResponse = await _userRepository.Save(cancellationToken);

        if (saveResponse is 0) return Guid.Empty;

        return user.Id;
    }
}