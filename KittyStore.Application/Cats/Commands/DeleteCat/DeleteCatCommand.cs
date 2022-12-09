using ErrorOr;
using KittyStore.Domain.CatAggregate.ValueObjects;
using MediatR;

namespace KittyStore.Application.Cats.Commands.DeleteCat;

public record DeleteCatCommand(CatId Id) : IRequest<ErrorOr<Unit>>;
