using KittyStore.Application.Common.Interfaces.Persistence;
using KittyStore.Domain.CatAggregate;
using ErrorOr;
using KittyStore.Domain.CatAggregate.Enums;
using KittyStore.Domain.Common.Errors;
using MediatR;

namespace KittyStore.Application.Cats.Commands.UpdateCat
{
    public class UpdateCatCommandHandler : IRequestHandler<UpdateCatCommand, ErrorOr<Cat>>
    {
        private readonly ICatRepository _catRepository;

        public UpdateCatCommandHandler(ICatRepository catRepository)
        {
            _catRepository = catRepository;
        }

        public async Task<ErrorOr<Cat>> Handle(UpdateCatCommand command, CancellationToken cancellationToken)
        {
            if (await _catRepository.GetCatByIdAsync(command.Id) is not {} cat)
                return Errors.Cat.NotFound;

            var updateCat = cat.Update(command.Name, command.Age, command.Color, command.Breed, 
                command.Price, (CatGender)Enum.Parse(typeof(CatGender) ,command.Gender, true));
            await _catRepository.UpdateCatAsync(updateCat);

            return cat;
        }
    }
}