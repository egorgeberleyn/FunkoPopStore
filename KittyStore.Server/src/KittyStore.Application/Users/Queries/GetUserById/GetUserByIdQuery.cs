using ErrorOr;
using KittyStore.Domain.UserAggregate;
using MediatR;

namespace KittyStore.Application.Users.Queries.GetUserById;

public record GetUserByIdQuery(Guid Id) : IRequest<ErrorOr<User>>;