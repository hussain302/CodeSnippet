using MediatR;
using Microsoft.AspNetCore.Mvc;
using CodeSnippet.API.Contracts;
using CodeSnippet.Application.Dtos;
using CodeSnippet.Domain.Primitives;
using CodeSnippet.API.Infrastructure;
using CodeSnippet.Application.Modules.Auth.Queries;

namespace CodeSnippet.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
public class AuthController(IMediator _mediator) : ApiController(mediator: _mediator ?? throw new ArgumentNullException("Mediator cant be null"))
{
    [HttpGet(ApiRoutes.Auth.Login)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserByUserNameOrEmailAndPassword(
        string? Username,
        string? Email,
        string Password, 
        CancellationToken cancellationToken)
    {
        LoginResponseDto? loginResponse = await _mediator.Send(
            new GetLoginUserQuery(Username, Email, Password),
            cancellationToken);

        if (loginResponse.Id != Guid.Empty)        
            return StatusCode(
                statusCode: StatusCodes.Status200OK,
                value: new ApiResult(
                    success: true,
                    message: "User has been logged in successfully",
                    result: loginResponse
                ));
        
        return StatusCode(
            statusCode: StatusCodes.Status404NotFound,
            value: new ApiResult(
                success: true,
                message: $"Incorrect Username, Email or Password, Please try again with correct one",
                result: new { }
            ));
    }
}
