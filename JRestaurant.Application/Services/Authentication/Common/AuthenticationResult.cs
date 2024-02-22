using JRestaurant.Domain.Entities;

namespace JRestaurant.Application.Services.Authentication.Common;

public record AuthenticationResult(
    User user,
    string Token
);
