using ErrorOr;
using JRestaurant.Application.Authentication.Commands.Queries.Login;
using JRestaurant.Application.Authentication.Commands.Register;
using JRestaurant.Application.Services.Authentication.Common;
using JRestaurant.Contracts.Authentication;
using JRestaurant.Domain.Common.Errors;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JRestaurant.Api.Controllers;

[Route("auth")]
public class AuthenticationController : ApiController
{
    private readonly ISender mediator;

    public AuthenticationController(ISender mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = new RegisterCommand(request.FirstName, request.LastName, request.Email, request.Password);
        ErrorOr<AuthenticationResult> authRequest = await mediator.Send(command);

        return authRequest.Match(
                 authResult => Ok(MapAuthResult(authResult)),
                 errors => Problem(errors)
             );
    }

    private AuthenticationResponse MapAuthResult(AuthenticationResult value)
    {
        return new AuthenticationResponse(
                        value.user.Id,
                        value.user.FirstName,
                        value.user.LastName,
                        value.user.Email,
                        value.Token
                );

    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = new LoginQuery(request.Email, request.Password);
        ErrorOr<AuthenticationResult> authResult = await mediator.Send(query);

        if (authResult.IsError && authResult.FirstError == Errors.Authentication.InvalidCredentials)
            return Problem(statusCode: StatusCodes.Status401Unauthorized, title: authResult.FirstError.Description);

        return authResult.Match(
                 authResult => Ok(MapAuthResult(authResult)),
                 errors => Problem(errors)
             );
    }


}