using KittyStore.Domain.OrderAggregate;

namespace KittyStore.Application.Common.Interfaces.Persistence
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetUserOrdersAsync(Guid id);

        Task CreateOrderAsync(Order newOrder);
    }
}