using ErrorOr;
using KittyStore.Application.Common.SaveChangesPostProcessor;
using MediatR;

namespace KittyStore.Application.Orders.Commands.ChangeStatus;

public record ChangeStatusCommand(
    Guid Id, 
    string Status) : IRequest<ErrorOr<Success>>, ICommand;

