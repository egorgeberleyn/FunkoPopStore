using MediatR;
using ErrorOr;
using KittyStore.Application.Common.Interfaces.Persistence;
using KittyStore.Domain.CatAggregate;
using KittyStore.Domain.Common.Errors;

namespace KittyStore.Application.Cats.Queries.GetCat;

public class GetCatQueryHandler : IRequestHandler<GetCatQuery, ErrorOr<Cat>>
{
    private readonly ICatRepository _catRepository;

    public GetCatQueryHandler(ICatRepository catRepository)
    {
        _catRepository = catRepository;
    }

    public async Task<ErrorOr<Cat>> Handle(GetCatQuery query, CancellationToken cancellationToken)
    {
        var cat = await _catRepository.GetCatByIdAsync(query.Id);

        if (cat is null)
            return Errors.Cat.NotFound;
        
        return cat;
    }
}