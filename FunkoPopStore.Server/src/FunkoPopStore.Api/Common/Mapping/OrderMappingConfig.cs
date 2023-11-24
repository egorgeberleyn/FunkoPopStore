using FunkoPopStore.Application.Orders.Commands.CreateOrder;
using FunkoPopStore.Contracts.Orders;
using FunkoPopStore.Domain.OrderAggregate;
using FunkoPopStore.Domain.OrderAggregate.Entities;
using Mapster;

namespace FunkoPopStore.Api.Common.Mapping;

public class OrderMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateOrderRequest, CreateOrderCommand>()
            .Map(dist => dist.AddressCommand, src => src.Address);

        config.NewConfig<Order, OrderResponse>()
            .Map(dist => dist.Address, src => src.Address);

        config.NewConfig<OrderItem, OrderItemResponse>();
    }
}