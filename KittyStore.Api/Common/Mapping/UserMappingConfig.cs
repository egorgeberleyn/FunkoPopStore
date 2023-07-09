using KittyStore.Application.Users.Commands.AddBalanceToUser;
using KittyStore.Application.Users.Commands.UpdateUser;
using KittyStore.Contracts.Admin.Users;
using KittyStore.Contracts.Profile;
using KittyStore.Domain.UserAggregate;
using Mapster;

namespace KittyStore.Api.Common.Mapping
{
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
}