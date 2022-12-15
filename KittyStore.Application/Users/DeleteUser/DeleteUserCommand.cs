﻿using ErrorOr;
using KittyStore.Domain.UserAggregate.ValueObjects;
using MediatR;

namespace KittyStore.Application.Users.DeleteUser;

public record DeleteUserCommand(UserId Id) : IRequest<ErrorOr<Unit>>;