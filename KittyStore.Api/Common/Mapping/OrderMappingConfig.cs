using KittyStore.Application.Orders.Commands.CreateOrder;
using KittyStore.Contracts.Orders;
using KittyStore.Domain.OrderAggregate;
using KittyStore.Domain.OrderAggregate.Entities;
using Mapster;

namespace KittyStore.Api.Common.Mapping;

public class OrderMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateOrderRequest, CreateOrderCommand>()
            .Map(dist => dist.AddressCommand, src => src.Address)
            .Map(dist => dist.UserId.Value, src => src.UserId);

        config.NewConfig<Order, OrderResponse>()
            .Map(dist => dist.UserId, src => src.UserId.Value)
            .Map(dist => dist.Id, src => src.Id.Value)
            .Map(dist => dist.Address, src => src.Address);
        
        config.NewConfig<OrderItem, OrderItemResponse>()
            .Map(dist => dist.CatId, src => src.CatId.Value)
            .Map(dist => dist.Id, src => src.Id.Value);
    }
}