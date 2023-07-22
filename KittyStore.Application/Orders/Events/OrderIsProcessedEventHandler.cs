using KittyStore.Domain.OrderAggregate.Events;
using MediatR;

namespace KittyStore.Application.Orders.Events;

public class OrderIsProcessedEventHandler : INotificationHandler<OrderIsProcessed>
{
    public Task Handle(OrderIsProcessed notification, CancellationToken cancellationToken)
    {
        //more logic (email send, create another entity etc.)
        Console.WriteLine("Order is processed");
        return Task.CompletedTask;
    }
}