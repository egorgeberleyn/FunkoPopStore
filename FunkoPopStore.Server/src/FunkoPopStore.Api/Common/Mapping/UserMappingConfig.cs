using FunkoPopStore.Application.Users.Commands.AddBalanceToUser;
using FunkoPopStore.Application.Users.Commands.UpdateUser;
using FunkoPopStore.Contracts.Admin.Users;
using FunkoPopStore.Contracts.Profile;
using FunkoPopStore.Domain.UserAggregate;
using Mapster;

namespace FunkoPopStore.Api.Common.Mapping;

public class UserMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UpdateUserRequest, UpdateUserCommand>()
            .Map(dist => dist.Balance, src => src.Balance);

        config.NewConfig<AddBalanceToUserRequest, AddBalanceToUserCommand>();

        config.NewConfig<User, UserResponse>()
            .Map(dist => dist.Role, src => src.Role.ToString())
            .Map(dist => dist.Balance, src => src.Balance);
    }
}