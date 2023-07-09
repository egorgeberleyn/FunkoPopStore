using MediatR;
using ErrorOr;
using KittyStore.Application.Common.Interfaces.Persistence;
using KittyStore.Domain.Common.Errors;

namespace KittyStore.Application.Cats.Commands.DeleteCat
{
    public class DeleteCatCommandHandler : IRequestHandler<DeleteCatCommand, ErrorOr<Unit>>
    {
        private readonly ICatRepository _catRepository;

        public DeleteCatCommandHandler(ICatRepository catRepository)
        {
            _catRepository = catRepository;
        }
    
        public async Task<ErrorOr<Unit>> Handle(DeleteCatCommand command, CancellationToken cancellationToken)
        {
            if (await _catRepository.GetCatByIdAsync(command.Id) is not {} cat)
                return Errors.Cat.NotFound;
        
            _catRepository.DeleteCat(cat);
            return Unit.Value;
        }
    }
}