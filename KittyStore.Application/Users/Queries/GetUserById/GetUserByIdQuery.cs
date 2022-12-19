using ErrorOr;
using KittyStore.Domain.UserAggregate;
using KittyStore.Domain.UserAggregate.ValueObjects;
using MediatR;

namespace KittyStore.Application.Users.Queries.GetUserById;

public record GetUserByIdQuery(UserId Id) : IRequest<ErrorOr<User>>;
