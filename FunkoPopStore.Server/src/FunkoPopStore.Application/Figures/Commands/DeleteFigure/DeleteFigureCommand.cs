using ErrorOr;
using FunkoPopStore.Application.Common.SaveChangesPostProcessor;
using MediatR;

namespace FunkoPopStore.Application.Figures.Commands.DeleteFigure;

public record DeleteFigureCommand(Guid Id) : IRequest<ErrorOr<Unit>>, ICommand;