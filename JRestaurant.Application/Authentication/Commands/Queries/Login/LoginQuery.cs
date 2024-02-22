using ErrorOr;
using JRestaurant.Application.Services.Authentication.Common;
using MediatR;

namespace JRestaurant.Application.Authentication.Commands.Queries.Login;

public record LoginQuery(string Email,
                           string Password) : IRequest<ErrorOr<AuthenticationResult>>;