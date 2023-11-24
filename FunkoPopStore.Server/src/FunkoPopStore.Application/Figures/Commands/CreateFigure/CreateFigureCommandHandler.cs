using ErrorOr;
using FunkoPopStore.Application.Common.Interfaces.Persistence;
using FunkoPopStore.Domain.FigureAggregate;
using FunkoPopStore.Domain.FigureAggregate.Enums;
using MediatR;

namespace FunkoPopStore.Application.Figures.Commands.CreateFigure;

public class CreateFigureCommandHandler : IRequestHandler<CreateFigureCommand, ErrorOr<Figure>>
{
    private readonly IFigureRepository _figureRepository;

    public CreateFigureCommandHandler(IFigureRepository figureRepository)
    {
        _figureRepository = figureRepository;
    }

    public async Task<ErrorOr<Figure>> Handle(CreateFigureCommand command, CancellationToken cancellationToken)
    {
        var rarity = (Rarity)Enum.Parse(typeof(Rarity), command.Rarity, true);
        var newFigure = Figure.Create(command.Name, command.Price, rarity,
            command.ProductionYear, command.SeriesId);

        await _figureRepository.CreateFigureAsync(newFigure);

        return newFigure;
    }
}