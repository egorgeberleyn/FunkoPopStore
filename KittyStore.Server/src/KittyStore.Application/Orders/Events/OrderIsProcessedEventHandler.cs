using KittyStore.Application.Common.Interfaces.Authentication;
using KittyStore.Application.Common.Interfaces.Email;
using KittyStore.Domain.OrderAggregate.Events;
using MediatR;

namespace KittyStore.Application.Orders.Events;

public class OrderIsProcessedEventHandler : INotificationHandler<OrderIsProcessed>
{
    private readonly IEmailService _emailService;
    private readonly ICurrentUserService _currentUserService;

    public OrderIsProcessedEventHandler(IEmailService emailService, ICurrentUserService currentUserService)
    {
        _emailService = emailService;
        _currentUserService = currentUserService;
    }

    public async Task Handle(OrderIsProcessed notification, CancellationToken cancellationToken)
    {
        var user = await _currentUserService.GetUserAsync();
        if(user is null) return;
        await _emailService.SendAsync(user.Email, "", $"Заказ №{notification.Order.Id} оформлен");
    }
}