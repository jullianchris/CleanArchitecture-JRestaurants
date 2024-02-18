using JRestaurant.Application.Common.Interfaces.Services;

namespace JRestaurant.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}