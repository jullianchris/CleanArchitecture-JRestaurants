using ErrorOr;
using JRestaurant.Application.Authentication.Commands.Queries.Login;
using JRestaurant.Application.Authentication.Commands.Register;
using JRestaurant.Application.Services.Authentication.Common;
using JRestaurant.Contracts.Authentication;
using JRestaurant.Domain.Common.Errors;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JRestaurant.Api.Controllers;

[Route("auth")]
[AllowAnonymous]
public class AuthenticationController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public AuthenticationController(ISender mediator, IMapper mapper)
    {
        this._mediator = mediator;
        this._mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = _mapper.Map<RegisterCommand>(request);
        //var command = new RegisterCommand(request.FirstName, request.LastName, request.Email, request.Password);
        ErrorOr<AuthenticationResult> authRequest = await _mediator.Send(command);

        return authRequest.Match(
                 authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
                 errors => Problem(errors)
             );
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        //var query = new LoginQuery(request.Email, request.Password);
        var query = _mapper.Map<LoginQuery>(request);
        ErrorOr<AuthenticationResult> authResult = await _mediator.Send(query);

        if (authResult.IsError && authResult.FirstError == Errors.Authentication.InvalidCredentials)
            return Problem(statusCode: StatusCodes.Status401Unauthorized, title: authResult.FirstError.Description);

        return authResult.Match(
                 authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
                 errors => Problem(errors)
             );
    }


}