using JRestaurant.Domain.Common.Errors;
using ErrorOr;
using JRestaurant.Application.Common.Interfaces.Authentication;
using JRestaurant.Application.Common.Interfaces.Persistence;
using JRestaurant.Application.Services.Authentication.Common;
using MediatR;
using JRestaurant.Domain.Entities;

namespace JRestaurant.Application.Authentication.Commands.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public LoginQueryHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator = null)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            // validate user
            if (_userRepository.GetUserByEmail(query.Email) is not User user)
            {
                return Errors.Authentication.InvalidCredentials;
            }

            if (user.Password != query.Password)
            {
                return Errors.Authentication.InvalidCredentials;
            }

            var token = _jwtTokenGenerator.GenerateToken(user);


            return new AuthenticationResult(
                        user,
                        token
                    );
        }
    }
}