using ErrorOr;
using FunkoPopStore.Application.Common.Interfaces.Persistence;
using FunkoPopStore.Domain.UserAggregate;
using MediatR;

namespace FunkoPopStore.Application.Users.Queries.GetAllUsers;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, ErrorOr<List<User>>>
{
    private readonly IUserRepository _userRepository;

    public GetAllUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<List<User>>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken) =>
        await _userRepository.GetAllUsers();
}