using ErrorOr;
using KittyStore.Application.Common.Interfaces.Persistence;
using KittyStore.Domain.CatAggregate;
using KittyStore.Domain.CatAggregate.Enums;
using MediatR;

namespace KittyStore.Application.Cats.Commands.CreateCat
{
    public class CreateCatCommandHandler : IRequestHandler<CreateCatCommand, ErrorOr<Cat>>
    {
        private readonly ICatRepository _catRepository;

        public CreateCatCommandHandler(ICatRepository catRepository)
        {
            _catRepository = catRepository;
        }

        public async Task<ErrorOr<Cat>> Handle(CreateCatCommand command, CancellationToken cancellationToken)
        {
            var cat = Cat.Create(command.Name, command.Age, command.Color, 
                command.Breed, command.Price, (CatGender)Enum.Parse(typeof(CatGender) ,command.Gender, true));

            await _catRepository.CreateCatAsync(cat);

            return cat;
        }
    }
}