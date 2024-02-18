using ErrorOr;
using FluentResults;
using JRestaurant.Application.Common.Errors;
using JRestaurant.Application.Common.Interfaces.Authentication;
using JRestaurant.Application.Common.Interfaces.Persistence;
using JRestaurant.Domain.Common.Errors;
using JRestaurant.Domain.Entities;

namespace JRestaurant.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public ErrorOr<AuthenticationResult> Login(string email, string password)
    {
        // validate user
        if (_userRepository.GetUserByEmail(email) is not User user)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        if (user.Password != password)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        var token = _jwtTokenGenerator.GenerateToken(user);


        return new AuthenticationResult(
                    user,
                    token
                );
    }

    public Result<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
    {
        // validate user
        if (_userRepository.GetUserByEmail(email) is not null)
        {
            return FluentResults.Result.Fail<AuthenticationResult>(new[] { new DuplicateEmailError() });
        }

        // create new user
        var user = new User
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password
        };

        _userRepository.Add(user);
        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user,
            token
        );
    }
}