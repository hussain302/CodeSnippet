using MediatR;
using CodeSnippet.Application.Modules.Users.Commands;
using CodeSnippet.Domain.Abstractions.Repositories;
using CodeSnippet.Domain.Aggregates;

namespace CodeSnippet.Application.Modules.Roles.Handlers;
public class UserCommandHandlers(IUserRepository userRepository) :
        IRequestHandler<CreateUserCommand, Guid>,
        IRequestHandler<UpdateUserCommand, bool>,
        IRequestHandler<DeleteUserCommand, bool>
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        User? user = new ()
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
            //Role = request.Role,
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

    public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.Find(id: request.Id, cancellationToken);

        if (user == null) return false;

        var existingUserWithSameEmail = await _userRepository.Get(
            u => (u.Email == request.Email && u.Id != request.Id),
                cancellationToken: cancellationToken
            );

        var existingUserWithSameUserName = await _userRepository.Get(
            u => (u.Username == request.Username && u.Id != request.Id),
                cancellationToken: cancellationToken
            );
        
        if (existingUserWithSameUserName.Any()) throw new Exception("A user with the requested username already exists");        
        
        if (existingUserWithSameEmail.Any()) throw new Exception("A user with the requested email already exists");        

        user.Username = request.Username;
        user.FirstName = request.FirstName;
        user.MiddleName = request.MiddleName;
        user.LastName = request.LastName;
        user.PasswordHash = request.Password;
        user.Email = request.Email;
        user.Address = request.Address;
        user.RoleId = request.Role.Id;
        user.UpdatedAt = DateTime.Now;

        await _userRepository.Update(user, cancellationToken);

        int saveResponse = await _userRepository.Save(cancellationToken);

        if(saveResponse is 0) return false;

        return true;
    }

    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.Find(id: request.Id, cancellationToken);

        if (user != null) await _userRepository.Delete(user, cancellationToken);

        int saveResponse = await _userRepository.Save(cancellationToken);

        if (saveResponse is 0) return false;

        return true;
    }
}