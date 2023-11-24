using ErrorOr;
using FunkoPopStore.Application.Common.Interfaces.Persistence;
using FunkoPopStore.Domain.FigureAggregate;
using MediatR;

namespace FunkoPopStore.Application.Figures.Queries.GetAllFigures;

public class GetAllFiguresQueryHandler : IRequestHandler<GetAllFiguresQuery, ErrorOr<List<Figure>>>
{
    private readonly IFigureRepository _figureRepository;

    public GetAllFiguresQueryHandler(IFigureRepository figureRepository)
    {
        _figureRepository = figureRepository;
    }

    public async Task<ErrorOr<List<Figure>>> Handle(GetAllFiguresQuery query, CancellationToken cancellationToken)
    {
        return await _figureRepository.GetAllFiguresAsync();
    }
}