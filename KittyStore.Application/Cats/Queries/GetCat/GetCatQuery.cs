using ErrorOr;
using KittyStore.Domain.CatAggregate;
using KittyStore.Domain.CatAggregate.ValueObjects;
using MediatR;

namespace KittyStore.Application.Cats.Queries.GetCat;

public record GetCatQuery(CatId Id) : IRequest<ErrorOr<Cat>>;
