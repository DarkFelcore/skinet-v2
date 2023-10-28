using ErrorOr;

namespace SkinetV2.Domain.Common.Errors
{
    public partial class Errors
    {
        public static class Users
        {
            public static Error DuplicateEmailAddress => Error.Validation(
                code: "Users.DuplicateEmailAddress",
                description: "The specified email address is already in use"
            );

            public static Error InvalidCredentials => Error.Unauthorized(
                code: "Users.InvalidCredentials",
                description: "Invalid credentials"
            );
        }
    }
}