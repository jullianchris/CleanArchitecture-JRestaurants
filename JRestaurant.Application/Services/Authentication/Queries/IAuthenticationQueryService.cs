using ErrorOr;
using FluentResults;
using JRestaurant.Application.Services.Authentication.Common;

namespace JRestaurant.Application.Services.Authentication.Queries;

public interface IAuthenticationQueryService
{
    ErrorOr<AuthenticationResult> Login(string email, string password);
}