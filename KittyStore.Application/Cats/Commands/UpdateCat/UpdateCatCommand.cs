using KittyStore.Domain.CatAggregate;
using MediatR;
using ErrorOr;
using KittyStore.Application.Common.SaveChangesPostProcessor;

namespace KittyStore.Application.Cats.Commands.UpdateCat
{
    public record UpdateCatCommand : IRequest<ErrorOr<Cat>>, ICommand
    {
        public Guid Id { get; set; }
        public string Name { get; init; } = default!;
        public int Age { get; init; }
        public string Color { get; init; } = default!;
        public string Breed { get; init; } = default!;
        public string Gender { get; init; } = default!;
        public decimal Price { get; init; } 
    }
}