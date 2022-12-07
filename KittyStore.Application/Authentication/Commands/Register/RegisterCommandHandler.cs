using MediatR;
using ErrorOr;
using KittyStore.Application.Authentication.Common;

namespace KittyStore.Application.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthResult>>
{
    public Task<ErrorOr<AuthResult>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}