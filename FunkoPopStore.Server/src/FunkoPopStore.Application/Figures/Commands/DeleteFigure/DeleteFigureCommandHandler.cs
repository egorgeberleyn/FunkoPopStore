using ErrorOr;
using FunkoPopStore.Application.Common.Interfaces.Persistence;
using FunkoPopStore.Domain.Common.Errors;
using MediatR;

namespace FunkoPopStore.Application.Figures.Commands.DeleteFigure;

public class DeleteFigureCommandHandler : IRequestHandler<DeleteFigureCommand, ErrorOr<Unit>>
{
    private readonly IFigureRepository _figureRepository;

    public DeleteFigureCommandHandler(IFigureRepository figureRepository)
    {
        _figureRepository = figureRepository;
    }

    public async Task<ErrorOr<Unit>> Handle(DeleteFigureCommand command, CancellationToken cancellationToken)
    {
        if (await _figureRepository.GetFigureByIdAsync(command.Id) is not { } figure)
            return Errors.Figure.NotFound;

        _figureRepository.DeleteFigure(figure);
        return Unit.Value;
    }
}