using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CodeSnippet.API.Infrastructure;

[ApiController]
public class ApiController : ControllerBase
{
    protected ApiController(IMediator mediator) => Mediator = mediator;

    protected IMediator Mediator { get; }

}
