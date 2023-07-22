using KittyStore.Domain.CatAggregate;
using MediatR;
using ErrorOr;
using KittyStore.Application.Common.SaveChangesPostProcessor;

namespace KittyStore.Application.Cats.Commands.UpdateCat
{
    public record UpdateCatCommand() : IRequest<ErrorOr<Cat>>, ICommand
    {
        public Guid Id { get; set; }
        public string? Name { get; init; }
        public int Age { get; init; }
        public string? Color { get; init; }
        public string? Breed { get; init; }
        public string? Gender { get; init; }
        public decimal Price { get; init; } 
    }
}