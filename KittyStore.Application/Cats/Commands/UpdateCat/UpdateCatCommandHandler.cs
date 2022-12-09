using KittyStore.Application.Common.Interfaces.Persistence;
using KittyStore.Domain.CatAggregate;
using ErrorOr;
using KittyStore.Domain.Common.Errors;
using MediatR;

namespace KittyStore.Application.Cats.Commands.UpdateCat;

public class UpdateCatCommandHandler : IRequestHandler<UpdateCatCommand, ErrorOr<Cat>>
{
    private readonly ICatRepository _catRepository;

    public UpdateCatCommandHandler(ICatRepository catRepository)
    {
        _catRepository = catRepository;
    }

    public async Task<ErrorOr<Cat>> Handle(UpdateCatCommand request, CancellationToken cancellationToken)
    {
        var cat = await _catRepository.GetCatByIdAsync(request.Id);

        if (cat is null)
            return Errors.Cat.NotFound;

        var updateCat = Cat.Update(cat, request.Name, request.Age, request.Color, request.Breed, request.Price);
        await _catRepository.UpdateCatAsync(updateCat);

        return cat;
    }
}