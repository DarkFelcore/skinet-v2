using ErrorOr;

namespace SkinetV2.Domain.Common.Errors
{
    public partial class Errors
    {
        public static class Product
        {
            public static Error NotFound => Error.NotFound(
                code: "Product.NotFound",
                description: "No product(s) found."
            );
        }
    }
}