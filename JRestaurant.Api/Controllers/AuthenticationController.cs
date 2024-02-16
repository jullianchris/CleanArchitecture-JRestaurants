using JRestaurant.Application.Services.Authentication;
using JRestaurant.Contracts.Authentication;
using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.AspNetCore.Mvc;

namespace JRestaurant.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authService;

    public AuthenticationController(IAuthenticationService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        var authRequest = _authService.Register(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password
        );

        var response = new AuthenticationResponse(
                authRequest.id,
                authRequest.FirstName,
                authRequest.LastName,
                authRequest.Email,
                authRequest.Token
        );

        return Ok(response);
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var authResult = _authService.Login(request.Email, request.Password);
        var response = new AuthenticationResponse(
                                authResult.id,
                                authResult.FirstName,
                                authResult.LastName,
                                authResult.Email,
                                authResult.Token
                        );

        return Ok(response);
    }


}