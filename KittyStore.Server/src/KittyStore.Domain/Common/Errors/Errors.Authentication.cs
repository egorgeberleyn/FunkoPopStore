using ErrorOr;

namespace KittyStore.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class Authentication
        {
            public static Error InvalidCredentials => Error.Validation(
                code: "Auth.InvalidCred",
                description: "Invalid credentials");
            
            public static Error InvalidRefreshToken => Error.Conflict(
                code: "Auth.InvalidRefreshToken",
                description: "Invalid refresh token");
            
            public static Error InvalidAccessToken => Error.Conflict(
                code: "Auth.InvalidAccessToken",
                description: "Invalid access jwt token");
            
            public static Error ExpiredAccessToken => Error.Conflict(
                code: "Auth.ExpiredAccessToken",
                description: "Expired access jwt token");
            
            public static Error ExpiredRefreshToken => Error.Conflict(
                code: "Auth.ExpiredRefreshToken",
                description: "Expired refresh token");
        }
    }
}