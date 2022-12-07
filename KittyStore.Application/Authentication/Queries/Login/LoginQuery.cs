﻿using KittyStore.Application.Authentication.Common;
using MediatR;
using ErrorOr;

namespace KittyStore.Application.Authentication.Queries.Login;

public record LoginQuery(
    string Email,
    string Password) : IRequest<ErrorOr<AuthenticationResult>>;
