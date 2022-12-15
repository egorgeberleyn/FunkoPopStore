using KittyStore.Domain.CatAggregate;
using MediatR;
using ErrorOr;
using KittyStore.Domain.CatAggregate.ValueObjects;

namespace KittyStore.Application.Cats.Commands.UpdateCat;

public record UpdateCatCommand(
    CatId Id,
    string Name,
    int Age,
    string Color,
    string Breed,
    string Gender,
    decimal Price) : IRequest<ErrorOr<Cat>>;