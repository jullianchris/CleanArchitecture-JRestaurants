using JRestaurant.Domain.Entities;

namespace JRestaurant.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    User? GetUserByEmail(string email);
    void Add(User user);
}