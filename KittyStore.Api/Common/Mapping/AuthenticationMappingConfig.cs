using KittyStore.Application.Authentication.Commands.Register;
using KittyStore.Application.Authentication.Common;
using KittyStore.Application.Authentication.Queries.Login;
using KittyStore.Contracts.Authentication;
using Mapster;

namespace KittyStore.Api.Common.Mapping
{
    public class AuthenticationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<RegisterRequest, RegisterCommand>();
        
            config.NewConfig<LoginRequest, LoginQuery>();
        
            config.NewConfig<AuthenticationResult, AuthenticationResponse>()
                .Map(dest => dest.Id, src => src.User.Id.Value)
                .Map(dest => dest.Role, src => src.User.Role.ToString())
                .Map(dest => dest, src => src.User);
        }
    }
}