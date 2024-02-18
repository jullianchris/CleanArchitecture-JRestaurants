using JRestaurant.Domain.Entities;

namespace JRestaurant.Application.Services.Authentication;

public record AuthenticationResult(
    User user,
    string Token
);
