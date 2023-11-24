using ErrorOr;
using FunkoPopStore.Application.Common.Interfaces.Persistence;
using FunkoPopStore.Domain.Common.Errors;
using FunkoPopStore.Domain.FigureAggregate;
using FunkoPopStore.Domain.FigureAggregate.Enums;
using MediatR;

namespace FunkoPopStore.Application.Figures.Commands.UpdateFigure;

public class UpdateFigureCommandHandler : IRequestHandler<UpdateFigureCommand, ErrorOr<Figure>>
{
    private readonly IFigureRepository _figureRepository;

    public UpdateFigureCommandHandler(IFigureRepository figureRepository)
    {
        _figureRepository = figureRepository;
    }

    public async Task<ErrorOr<Figure>> Handle(UpdateFigureCommand command, CancellationToken cancellationToken)
    {
        if (await _figureRepository.GetFigureByIdAsync(command.Id) is not { } figure)
            return Errors.Figure.NotFound;

        var rarity = (Rarity)Enum.Parse(typeof(Rarity), command.Rarity, true);
        var updateFigure = figure.Update(command.Name, command.Price, rarity, command.SeriesId);
        _figureRepository.UpdateFigure(updateFigure);
        return updateFigure;
    }
}