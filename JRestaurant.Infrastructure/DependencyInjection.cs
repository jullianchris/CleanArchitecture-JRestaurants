using JRestaurant.Application.Common.Interfaces.Authentication;
using JRestaurant.Application.Common.Interfaces.Persistence;
using JRestaurant.Application.Common.Interfaces.Services;
using JRestaurant.Infrastructure.Authentication;
using JRestaurant.Infrastructure.Persistence;
using JRestaurant.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JRestaurant.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();


        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}