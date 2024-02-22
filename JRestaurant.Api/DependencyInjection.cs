using BuberDinner.Api.Common.Errors;
using JRestaurant.Api.Common.Mapping;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace JRestaurant.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            //builder.Services.AddControllers(options => options.Filters.Add<ErrorHandlingFilterAttribute>());
            services.AddControllers();
            services.AddSingleton<ProblemDetailsFactory, JRestaurantProblemDetailsFactory>();
            services.AddMappings();
            return services;
        }
    }
}