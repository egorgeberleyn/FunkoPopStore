using ErrorOr;

namespace KittyStore.Domain.Common.Errors;

public static partial class Errors
{
    public static class Order
    {
        public static Error NotFound => Error.NotFound(
            "Order.EmailNotFound",
            "Заказ не найден");
    }
}