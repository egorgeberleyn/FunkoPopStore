using ErrorOr;

namespace KittyStore.Domain.Common.Errors;

public partial class Errors
{
    public static class Cat
    {
        public static Error NotFound => Error.NotFound(
            code: "Cat.NotFound",
            description: "Cat not found in database.");
    }
}