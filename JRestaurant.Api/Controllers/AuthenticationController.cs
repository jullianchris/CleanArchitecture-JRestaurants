using ErrorOr;
using FluentResults;
using JRestaurant.Application.Common.Errors;
using JRestaurant.Application.Services.Authentication;
using JRestaurant.Contracts.Authentication;
using JRestaurant.Domain.Common.Errors;
using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.AspNetCore.Mvc;

namespace JRestaurant.Api.Controllers;

[Route("auth")]
public class AuthenticationController : ApiController
{
    private readonly IAuthenticationService _authService;

    public AuthenticationController(IAuthenticationService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        Result<AuthenticationResult> authRequest = _authService.Register(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password
        );

        if (authRequest.IsSuccess)
        {
            return Ok(MapAuthResult(authRequest.Value));
        }

        var firstError = authRequest.Errors[0];
        if (firstError is DuplicateEmailError)
            return Problem(statusCode: StatusCodes.Status409Conflict, detail: "Email already exists");

        return Problem();
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
    public IActionResult Login(LoginRequest request)
    {
        ErrorOr<AuthenticationResult> authResult = _authService.Login(request.Email, request.Password);

        if (authResult.IsError && authResult.FirstError == Errors.Authentication.InvalidCredentials)
            return Problem(statusCode: StatusCodes.Status401Unauthorized, title: authResult.FirstError.Description);

        return authResult.Match(
            authResult => Ok(MapAuthResult(authResult)),
            Errors => Problem(Errors)
        );
    }


}