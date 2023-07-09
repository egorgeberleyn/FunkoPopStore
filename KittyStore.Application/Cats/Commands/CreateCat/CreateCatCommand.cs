using MediatR;
using ErrorOr;
using KittyStore.Application.Common.Interfaces.Persistence;
using KittyStore.Application.Common.SaveChangesPostProcessor;
using KittyStore.Domain.CatAggregate;
using KittyStore.Domain.CatAggregate.Enums;

namespace KittyStore.Application.Cats.Commands.CreateCat
{
    public record CreateCatCommand(
        string Name,
        int Age,
        string Color,
        string Breed,
        string Gender,
        decimal Price) : IRequest<ErrorOr<Cat>>, ICommand;
    
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
