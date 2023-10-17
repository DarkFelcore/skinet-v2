using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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