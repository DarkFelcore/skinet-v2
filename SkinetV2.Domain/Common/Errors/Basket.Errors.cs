using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;

namespace SkinetV2.Domain.Common.Errors
{
    public partial class Errors
    {
        public static class Basket 
        {
            public static Error NotFound => Error.NotFound(
                code: "Basket.NotFound",
                description: "Basket not found."
            );

            public static Error DeleteFailed => Error.Conflict(
                code: "Basket.DeleteFailed",
                description: "Error while deleting the basket."
            );

            public static Error EmptyBasket => Error.NotFound(
                code: "Basket.EmptyBasket",
                description: "This basket is empty."
            );
        }
    }
}