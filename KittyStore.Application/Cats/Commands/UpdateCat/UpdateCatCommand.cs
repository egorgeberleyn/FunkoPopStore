using KittyStore.Domain.CatAggregate;
using MediatR;
using ErrorOr;
using KittyStore.Domain.CatAggregate.ValueObjects;

namespace KittyStore.Application.Cats.Commands.UpdateCat
{
    public record UpdateCatCommand : IRequest<ErrorOr<Cat>>
    {
        public CatId Id { get; set; } = default!;
        public string Name { get; init; } = default!;
        public int Age { get; init; } = default!;
        public string Color { get; init; } = default!;
        public string Breed { get; init; } = default!;
        public string Gender { get; init; } = default!;
        public decimal Price { get; init; } = default!;
    }
}