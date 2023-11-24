using ErrorOr;
using FunkoPopStore.Application.Common.Interfaces.Persistence;
using FunkoPopStore.Domain.Common.Errors;
using FunkoPopStore.Domain.FigureAggregate;
using MediatR;

namespace FunkoPopStore.Application.Figures.Queries.GetCat;

public class GetFigureQueryHandler : IRequestHandler<GetFigureQuery, ErrorOr<Figure>>
{
    private readonly IFigureRepository _figureRepository;

    public GetFigureQueryHandler(IFigureRepository figureRepository)
    {
        _figureRepository = figureRepository;
    }


    public async Task<ErrorOr<Figure>> Handle(GetFigureQuery query, CancellationToken cancellationToken)
    {
        var figure = await _figureRepository.GetFigureByIdAsync(query.Id);

        if (figure is null)
            return Errors.Figure.NotFound;

        return figure;
    }
}