using ErrorOr;
using FunkoPopStore.Application.Common.SaveChangesPostProcessor;
using FunkoPopStore.Domain.FigureAggregate;
using MediatR;

namespace FunkoPopStore.Application.Figures.Commands.CreateFigure;

public record CreateFigureCommand(
    string Name,
    Guid SeriesId,
    decimal Price,
    string Rarity,
    DateTime ProductionYear) : IRequest<ErrorOr<Figure>>, ICommand;