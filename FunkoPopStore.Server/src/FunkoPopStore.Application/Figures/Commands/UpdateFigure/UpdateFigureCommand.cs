using ErrorOr;
using FunkoPopStore.Application.Common.SaveChangesPostProcessor;
using FunkoPopStore.Domain.FigureAggregate;
using MediatR;

namespace FunkoPopStore.Application.Figures.Commands.UpdateFigure;

public record UpdateFigureCommand(
    Guid Id,
    string Name,
    Guid? SeriesId,
    decimal Price,
    string Rarity) : IRequest<ErrorOr<Figure>>, ICommand;