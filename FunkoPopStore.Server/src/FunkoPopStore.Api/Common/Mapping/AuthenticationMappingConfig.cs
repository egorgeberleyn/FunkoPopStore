using FunkoPopStore.Application.Authentication.Commands.Register;
using FunkoPopStore.Application.Authentication.Common;
using FunkoPopStore.Application.Authentication.Queries.Login;
using FunkoPopStore.Contracts.Authentication;
using Mapster;

namespace FunkoPopStore.Api.Common.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterRequest, RegisterCommand>();

        config.NewConfig<LoginRequest, LoginQuery>();

        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest.Role, src => src.User.Role.ToString())
            .Map(dest => dest, src => src.User);
    }
}