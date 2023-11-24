using ErrorOr;
using FunkoPopStore.Application.Common.SaveChangesPostProcessor;
using MediatR;

namespace FunkoPopStore.Application.Orders.Commands.ChangeStatus;

public record ChangeStatusCommand(
    Guid Id, 
    string Status) : IRequest<ErrorOr<Success>>, ICommand;

