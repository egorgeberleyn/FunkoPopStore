using ErrorOr;

namespace FunkoPopStore.Domain.Common.Errors;

public partial class Errors
{
    public static class Figure
    {
        public static Error NotFound => Error.NotFound(
            code: "Figure.NotFound",
            description: "Figure not found in database.");

        public static Error AlreadyExist => Error.Validation(
            code: "Figure.AlreadyExist",
            description: "Figure already exist in shopCart.");
    }
}