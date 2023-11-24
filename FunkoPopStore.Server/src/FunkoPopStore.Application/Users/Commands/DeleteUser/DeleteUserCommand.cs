using ErrorOr;
using FunkoPopStore.Application.Common.SaveChangesPostProcessor;
using MediatR;

namespace FunkoPopStore.Application.Users.Commands.DeleteUser;

public record DeleteUserCommand(Guid Id) : IRequest<ErrorOr<Unit>>, ICommand;