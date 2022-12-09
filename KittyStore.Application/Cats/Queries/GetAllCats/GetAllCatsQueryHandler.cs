using MediatR;
using ErrorOr;
using KittyStore.Application.Common.Interfaces.Persistence;
using KittyStore.Domain.CatAggregate;

namespace KittyStore.Application.Cats.Queries.GetAllCats;

public class GetAllCatsQueryHandler : IRequestHandler<GetAllCatsQuery, ErrorOr<List<Cat>>>
{
    private readonly ICatRepository _catRepository;

    public GetAllCatsQueryHandler(ICatRepository catRepository)
    {
        _catRepository = catRepository;
    }

    public async Task<ErrorOr<List<Cat>>> Handle(GetAllCatsQuery request, CancellationToken cancellationToken)
    {
        return await _catRepository.GetAllCatsAsync();
    }
}