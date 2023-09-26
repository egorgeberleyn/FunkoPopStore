using ErrorOr;
using KittyStore.Domain.CatAggregate;
using MediatR;

namespace KittyStore.Application.Cats.Queries.GetCat
{
    public record GetCatQuery(Guid Id) : IRequest<ErrorOr<Cat>>;
}