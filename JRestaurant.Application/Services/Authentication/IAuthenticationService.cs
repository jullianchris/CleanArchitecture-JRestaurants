using ErrorOr;
using FluentResults;

namespace JRestaurant.Application.Services.Authentication;

public interface IAuthenticationService
{
    ErrorOr<AuthenticationResult> Login(string email, string password);
    Result<AuthenticationResult> Register(string firstName, string lastName, string email, string password);
}