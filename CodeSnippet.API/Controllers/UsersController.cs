using MediatR;
using Microsoft.AspNetCore.Mvc;
using CodeSnippet.API.Contracts;
using CodeSnippet.Application.Dtos;
using CodeSnippet.Domain.Primitives;
using CodeSnippet.API.Infrastructure;
using CodeSnippet.Application.Modules.Users.Commands;
using CodeSnippet.Application.Modules.Users.Querys;

namespace CodeSnippet.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
public class UsersController(IMediator _mediator) : ApiController(mediator: _mediator ?? throw new ArgumentNullException("Mediator cant be null"))
{

    [HttpGet(ApiRoutes.User.GetAll)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserDto>))]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _mediator.Send(new GetAllUsersQuery());
        return StatusCode(
            statusCode: StatusCodes.Status200OK,
            value: new ApiResult(
                success: true,
                message: "Users data has been fetched successfully",
                result: users
            ));
    }

    [HttpGet(ApiRoutes.User.GetById)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserById([FromQuery] Guid id)
    {
        UserDto? user = await _mediator.Send(new GetUserByIdQuery(id));

        if (user.Id != Guid.Empty)
        {
            return StatusCode(
                statusCode: StatusCodes.Status200OK,
                value: new ApiResult(
                    success: true,
                    message: "User has been fetched successfully",
                    result: user
                ));
        }

        return StatusCode(
            statusCode: StatusCodes.Status404NotFound,
            value: new ApiResult(
                success: true,
                message: $"User does not exists against id '{id}'",
                result: new { }
            ));
    }

    [HttpPost(ApiRoutes.User.Post)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Guid))]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
    {
        if (!command.IsValidUser)
            return StatusCode(
               statusCode: StatusCodes.Status400BadRequest,
               value: new ApiResult(
                   success: true,
                   message: "Parameter (Username, FirstName, MiddleName, LastName, Password, Email, Role) cant be null",
                   result: new { }
               ));

        if (!command.IsValidEmail)
            return StatusCode(
               statusCode: StatusCodes.Status400BadRequest,
               value: new ApiResult(
                   success: true,
                   message: "Email is not a valid email",
                   result: new { sampleEmail = "example@xyz.com" }
               ));

        if (!command.IsValidPassword)
            return StatusCode(
               statusCode: StatusCodes.Status400BadRequest,
               value: new ApiResult(
                   success: false,
                   message: "Your password is not strong enough. It should contain at least 8 characters, including both uppercase and lowercase letters, and at least one digit.",
                   result: new { }
               ));

        var userId = await _mediator.Send(command);

        return StatusCode(
            statusCode: StatusCodes.Status201Created,
            value: new ApiResult(
                success: true,
                message: "User has been created successfully",
                result: new { createdId = userId }
            ));
    }

    [HttpPut(ApiRoutes.User.Update)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand command)
    {
        if (!command.IsValidUser)
            return StatusCode(
               statusCode: StatusCodes.Status400BadRequest,
               value: new ApiResult(
                   success: true,
                   message: "Parameter (Username, FirstName, MiddleName, LastName, Password, Email, Role) cant be null",
                   result: new { }
               ));

        if (!command.IsValidEmail)
            return StatusCode(
               statusCode: StatusCodes.Status400BadRequest,
               value: new ApiResult(
                   success: true,
                   message: "Email is not a valid email",
                   result: new { sampleEmail = "example@xyz.com" }
               ));

        if (!command.IsValidPassword)
            return StatusCode(
               statusCode: StatusCodes.Status400BadRequest,
               value: new ApiResult(
                   success: false,
                   message: "Your password is not strong enough. It should contain at least 8 characters, including both uppercase and lowercase letters, and at least one digit.",
                   result: new { }
               ));
        
        bool success = await _mediator.Send(command);

        if (success)
            return StatusCode(
                    statusCode: StatusCodes.Status200OK,
                    value: new ApiResult(
                        success: true,
                        message: "User has been updated successfully",
                        result:  command 
                    ));
        
        return StatusCode(
                statusCode: StatusCodes.Status404NotFound,
                value: new ApiResult(
                    success: true,
                    message: "Usere not found",
                    result: new { }
                ));
    }

    [HttpDelete(ApiRoutes.User.Delete)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteUser([FromQuery] DeleteUserCommand command)
    {
        if (command.Id == Guid.Empty)
            return StatusCode(
               statusCode: StatusCodes.Status400BadRequest,
               value: new ApiResult(
                   success: true,
                   message: "Parameter (Id) cant be null",
                   result: new { }
               ));

        bool success = await _mediator.Send(command);

        if (success)
        {
            return StatusCode(
                statusCode: StatusCodes.Status200OK,
                value: new ApiResult(
                    success: true,
                    message: "User has been deleted successfully",
                    result: new { deletedId = command.Id }
                ));
        }

        return StatusCode(
                statusCode: StatusCodes.Status404NotFound,
                value: new ApiResult(
                    success: true,
                    message: "User not found",
                    result: new { }
                ));
    }
}
