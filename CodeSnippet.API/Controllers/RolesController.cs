using MediatR;
using Microsoft.AspNetCore.Mvc;
using CodeSnippet.API.Contracts;
using CodeSnippet.Application.Dtos;
using CodeSnippet.Domain.Primitives;
using CodeSnippet.API.Infrastructure;
using CodeSnippet.Application.Modules.Roles.Querys;
using CodeSnippet.Application.Modules.Roles.Commands;

namespace CodeSnippet.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
public sealed class RolesController(IMediator _mediator) : ApiController(mediator: _mediator ?? throw new ArgumentNullException("Mediator cant be null"))
{

    [HttpGet(ApiRoutes.Role.GetAll)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RoleDto>))]
    public async Task<IActionResult> GetAllRoles()
    {
        var roles = await _mediator.Send(new GetAllRolesQuery());
        return StatusCode(
            statusCode: StatusCodes.Status200OK,
            value: new ApiResult(
                success: true,
                message: "Roles data has been fetched successfully",
                result: roles
            ));
    }

    [HttpPost(ApiRoutes.Role.Post)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Guid))]
    public async Task<IActionResult> CreateRole([FromBody] CreateRoleCommand command)
    {
        if (string.IsNullOrWhiteSpace(command.Name) || string.IsNullOrWhiteSpace(command.Description))
            return StatusCode(
               statusCode: StatusCodes.Status400BadRequest,
               value: new ApiResult(
                   success: true,
                   message: "Parameter (Id, Name, Description) cant be null",
                   result: new { }
               ));

        var roleId = await _mediator.Send(command);
        return StatusCode(
            statusCode: StatusCodes.Status201Created,
            value: new ApiResult(
                success: true,
                message: "Roles data has been created successfully",
                result: new { createdId = roleId }
            ));
    }

    [HttpGet(ApiRoutes.Role.GetById)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RoleDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetRoleById([FromQuery] Guid id)
    {
        RoleDto? role = await _mediator.Send(new GetRoleByIdQuery(id));

        if (role != null)
        {
            return StatusCode(
                statusCode: StatusCodes.Status200OK,
                value: new ApiResult(
                    success: true,
                    message: "Roles data has been fetched successfully",
                    result: role
                ));
        }

        return StatusCode(
            statusCode: StatusCodes.Status404NotFound,
            value: new ApiResult(
                success: true,
                message: $"Roles does not exists against id '{id}'",
                result: new { }
            ));
    }

    [HttpPut(ApiRoutes.Role.Update)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateRole([FromBody] UpdateRoleCommand command)
    {

        if (string.IsNullOrWhiteSpace(command.Name) || string.IsNullOrWhiteSpace(command.Description) || command.Id == Guid.Empty)
            return StatusCode(
               statusCode: StatusCodes.Status400BadRequest,
               value: new ApiResult(
                   success: true,
                   message: "Parameter (Id, Name, Description) cant be null",
                   result: new { }
               ));

        bool success = await _mediator.Send(command);

        if (success)
        {
            return StatusCode(
                statusCode: StatusCodes.Status200OK,
                value: new ApiResult(
                    success: true,
                    message: "Roles data has been updated successfully",
                    result: new { updatedRole = command }
                ));
        }

        return StatusCode(
                statusCode: StatusCodes.Status404NotFound,
                value: new ApiResult(
                    success: true,
                    message: "Role not found",
                    result: new { }
                ));
    }

    [HttpDelete(ApiRoutes.Role.Delete)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteRole([FromQuery] DeleteRoleCommand command)
    {
        if(command.Id == Guid.Empty)
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
                    message: "Roles has been deleted successfully",
                    result: new { deletedId = command.Id }
                ));
        }

        return StatusCode(
                statusCode: StatusCodes.Status404NotFound,
                value: new ApiResult(
                    success: true,
                    message: "Role not found",
                    result: new { }
                ));
    }

}
