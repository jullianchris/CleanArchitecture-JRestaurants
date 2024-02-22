using FluentResults;
using JRestaurant.Application.Common.Errors;
using JRestaurant.Application.Common.Interfaces.Authentication;
using JRestaurant.Application.Common.Interfaces.Persistence;
using JRestaurant.Application.Services.Authentication.Common;
using JRestaurant.Domain.Entities;

namespace JRestaurant.Application.Services.Authentication.Commands;

public class AuthenticationCommandService : IAuthenticationCommandService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public AuthenticationCommandService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
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