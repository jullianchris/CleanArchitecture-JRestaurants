using JRestaurant.Domain.Entities;

namespace JRestaurant.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}