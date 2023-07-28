using ErrorOr;
using KittyStore.Domain.UserAggregate;
using MediatR;

namespace KittyStore.Application.Users.Queries.GetAllUsers;

public record GetAllUsersQuery() : IRequest<ErrorOr<List<User>>>;