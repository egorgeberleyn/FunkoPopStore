using KittyStore.Domain.CatAggregate;
using MediatR;
using ErrorOr;

namespace KittyStore.Application.Cats.Queries.GetAllCats
{
    public record GetAllCatsQuery() : IRequest<ErrorOr<List<Cat>>>;
}
