using ErrorOr;

namespace SkinetV2.Domain.Common.Errors
{
    public partial class Errors
    {
        public static class DeliveryMedthod {
            public static Error NotFound => Error.NotFound(
                code: "DeliveryMedthod.NotFound",
                description: "Delivery medthod not found."
            );
        }
    }
}