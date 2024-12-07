using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversityApi.Features.Auth.Commands.Login;
using UniversityApi.Features.Auth.Commands.Register;

namespace UniversityApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public Task<LoginCommandResponse> Login(LoginCommand command) =>
        _mediator.Send(command);

    [HttpPost("register")]
    public Task<LoginCommandResponse> Register(RegisterCommand command) =>
        _mediator.Send(command);
}
