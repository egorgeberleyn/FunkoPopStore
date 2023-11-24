using ErrorOr;
using FunkoPopStore.Domain.UserAggregate;
using MediatR;

namespace FunkoPopStore.Application.Users.Queries.GetUserById;

public record GetUserByIdQuery(Guid Id) : IRequest<ErrorOr<User>>;