using MediatR;
using ErrorOr;
using KittyStore.Application.Common.Interfaces.Persistence;
using KittyStore.Domain.Common.Errors;

namespace KittyStore.Application.Cats.Commands.DeleteCat;

public class DeleteCatCommandHandler : IRequestHandler<DeleteCatCommand, ErrorOr<Unit>>
{
    private readonly ICatRepository _catRepository;

    public DeleteCatCommandHandler(ICatRepository catRepository)
    {
        _catRepository = catRepository;
    }
    
    public async Task<ErrorOr<Unit>> Handle(DeleteCatCommand request, CancellationToken cancellationToken)
    {
        var cat = await _catRepository.GetCatByIdAsync(request.Id);

        if (cat is null)
            return Errors.Cat.NotFound;
        
        await _catRepository.DeleteCatAsync(cat);

        return Unit.Value;
    }
}