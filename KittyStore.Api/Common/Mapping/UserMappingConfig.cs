using KittyStore.Application.Users.UpdateUser;
using KittyStore.Contracts.Users;
using KittyStore.Domain.UserAggregate;
using Mapster;

namespace KittyStore.Api.Common.Mapping;

public class UserMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UpdateUserRequest, UpdateUserCommand>()
            .Map(dist => dist.Id.Value, src => src.Id);
        
        config.NewConfig<User, UserResponse>()
            .Map(dist => dist.Id, src => src.Id.Value);
    }
}