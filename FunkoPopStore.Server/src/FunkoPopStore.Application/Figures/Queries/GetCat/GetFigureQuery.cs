using ErrorOr;
using FunkoPopStore.Domain.FigureAggregate;
using MediatR;

namespace FunkoPopStore.Application.Figures.Queries.GetCat;

public record GetFigureQuery(Guid Id) : IRequest<ErrorOr<Figure>>;