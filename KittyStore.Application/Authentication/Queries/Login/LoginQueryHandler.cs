using MediatR;
using ErrorOr;
using KittyStore.Application.Authentication.Common;

namespace KittyStore.Application.Authentication.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthResult>>
{
    public Task<ErrorOr<AuthResult>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}