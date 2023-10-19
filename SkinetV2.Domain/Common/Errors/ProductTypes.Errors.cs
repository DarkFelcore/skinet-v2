using ErrorOr;

namespace SkinetV2.Domain.Common.Errors
{
    public partial class Errors
    {
        public static class ProductTypes 
        {
            public static Error NotFound => Error.NotFound(
                code: "ProductTypes.NotFound",
                description: "No product types found."
            );
        }
    }
}