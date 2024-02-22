using ErrorOr;
using FluentResults;
using JRestaurant.Application.Services.Authentication.Common;

namespace JRestaurant.Application.Services.Authentication.Commands;

public interface IAuthenticationCommandService
{
    Result<AuthenticationResult> Register(string firstName, string lastName, string email, string password);
}