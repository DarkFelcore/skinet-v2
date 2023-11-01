using ErrorOr;

namespace SkinetV2.Domain.Common.Errors
{
    public partial class Errors
    {
        public static class Order
        {
            public static Error ProblemCreatingOrder => Error.Conflict(
                code: "Order.ProblemCreatingOrder",
                description: "Problem creating order"
            );

            public static Error NotFound => Error.NotFound(
                code: "Order.NotFound",
                description: "Order not found"
            );
        }
    }
}