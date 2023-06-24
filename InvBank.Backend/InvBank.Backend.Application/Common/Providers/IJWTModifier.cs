namespace InvBank.Backend.Application.Common.Providers;

public interface IJWTModifier
{
    string GenerateToken(Dictionary<string, string> claims, DateTime? expiresAt);
    Task<bool> ValidateTokenAsync(string token);
    Dictionary<string, string> GetClaims(string token);

}