    using SkinetV2.Domain.Users.ValueObjects;

    namespace SkinetV2.Domain.Users
    {
        public class User
        {
            public UserId UserId { get; set; } = null!;
            public required string FirstName { get; set; }
            public required string LastName { get; set; }
            public required string Email { get; set; }
            public required string PasswordHash { get; set; }
            public required Address Address { get; set; }
        }
    }