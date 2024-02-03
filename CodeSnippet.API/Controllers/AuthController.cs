using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using CodeSnippet.API.Infrastructure;
using CodeSnippet.API.Contracts;
using CodeSnippet.Application.Dtos;
using CodeSnippet.Application.Modules.Users.Querys;
using CodeSnippet.Domain.Primitives;

namespace CodeSnippet.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
public class AuthController(IMediator _mediator) : ApiController(mediator: _mediator ?? throw new ArgumentNullException("Mediator cant be null"))
{
    
    //[HttpGet(ApiRoutes.Auth.Login)]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public async Task<IActionResult> GetUserByUserNameOrEmailAndPassword([FromBody] LoginRequest loginRequest)
    //{
    //    LoginResponse? loginResponse = await _mediator.Send(new GetUserLoginRequestQuery(loginRequest));

    //    if (loginResponse.Id != Guid.Empty)
    //    {
    //        return StatusCode(
    //            statusCode: StatusCodes.Status200OK,
    //            value: new ApiResult(
    //                success: true,
    //                message: "User has been logged in successfully",
    //                result: user
    //            ));
    //    }

    //    return StatusCode(
    //        statusCode: StatusCodes.Status404NotFound,
    //        value: new ApiResult(
    //            success: true,
    //            message: $"Incorrect Username, Email or Password, Please try again with correct one",
    //            result: new { }
    //        ));
    //}




}
