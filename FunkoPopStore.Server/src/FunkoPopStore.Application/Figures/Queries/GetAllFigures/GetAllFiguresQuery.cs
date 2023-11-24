using ErrorOr;
using FunkoPopStore.Domain.FigureAggregate;
using MediatR;

namespace FunkoPopStore.Application.Figures.Queries.GetAllFigures;

public record GetAllFiguresQuery() : IRequest<ErrorOr<List<Figure>>>;