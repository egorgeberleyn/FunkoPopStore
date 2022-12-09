using MediatR;
using ErrorOr;
using KittyStore.Application.Common.Interfaces.Persistence;
using KittyStore.Domain.CatAggregate;

namespace KittyStore.Application.Cats.Commands.CreateCat;

public class CreateCatCommandHandler : IRequestHandler<CreateCatCommand, ErrorOr<Cat>>
{
    private readonly ICatRepository _catRepository;

    public CreateCatCommandHandler(ICatRepository catRepository)
    {
        _catRepository = catRepository;
    }

    public async Task<ErrorOr<Cat>> Handle(CreateCatCommand request, CancellationToken cancellationToken)
    {
        var cat = Cat.Create(request.Name, request.Age, request.Color, 
            request.Breed, request.Price);

        await _catRepository.CreateCatAsync(cat);

        return cat;
    }
}