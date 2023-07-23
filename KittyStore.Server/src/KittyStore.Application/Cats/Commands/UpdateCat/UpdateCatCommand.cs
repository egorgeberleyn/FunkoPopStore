using KittyStore.Domain.CatAggregate;
using MediatR;
using ErrorOr;
using KittyStore.Application.Common.SaveChangesPostProcessor;

namespace KittyStore.Application.Cats.Commands.UpdateCat
{
    public record UpdateCatCommand() : IRequest<ErrorOr<Cat>>, ICommand
    {
        public Guid Id { get; set; }
        public string Name { get; init; } = string.Empty;
        public int Age { get; init; } 
        public string Color { get; init; } = string.Empty;
        public string Breed { get; init; } = string.Empty;
        public string Gender { get; init; } = string.Empty;
        public decimal Price { get; init; } 
    }
}