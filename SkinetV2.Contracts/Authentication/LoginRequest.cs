namespace SkinetV2.Contracts.Authentication
{
    public record LoginRequest(
        string Email,
        string Password
    );
}