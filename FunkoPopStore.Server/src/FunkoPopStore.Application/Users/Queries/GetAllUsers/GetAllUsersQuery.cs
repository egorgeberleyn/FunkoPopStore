using ErrorOr;
using FunkoPopStore.Domain.UserAggregate;
using MediatR;

namespace FunkoPopStore.Application.Users.Queries.GetAllUsers;

public record GetAllUsersQuery() : IRequest<ErrorOr<List<User>>>;