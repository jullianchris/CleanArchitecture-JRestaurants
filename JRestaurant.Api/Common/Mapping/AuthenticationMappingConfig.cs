using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JRestaurant.Application.Authentication.Commands.Queries.Login;
using JRestaurant.Application.Authentication.Commands.Register;
using JRestaurant.Application.Services.Authentication.Common;
using JRestaurant.Contracts.Authentication;
using Mapster;

namespace JRestaurant.Api.Common.Mapping
{
    public class AuthenticationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<RegisterRequest, RegisterCommand>();

            config.NewConfig<LoginRequest, LoginQuery>();

            config.NewConfig<AuthenticationResult, AuthenticationResponse>()
                  .Map(dest => dest.Token, src => src.Token)
                  .Map(dest => dest, src => src.user);
        }
    }
}