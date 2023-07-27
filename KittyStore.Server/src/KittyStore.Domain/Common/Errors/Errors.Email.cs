using ErrorOr;

namespace KittyStore.Domain.Common.Errors;

public static partial class Errors
{
    public static class Email
    {
        public static Error EmailNotFound => Error.Validation(
            "Email.EmailNotFound",
            "Указанного email адреса не существует");
        
        public static Error EmailNotValid => Error.Validation(
            "Email.EmailNotValid",
            "Неверный email адрес");
    }
}