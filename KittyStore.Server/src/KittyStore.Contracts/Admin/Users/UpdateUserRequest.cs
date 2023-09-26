﻿namespace KittyStore.Contracts.Admin.Users
{
    public record UpdateUserRequest(
        string FirstName,
        string LastName,
        string Email,
        Balance Balance);
}