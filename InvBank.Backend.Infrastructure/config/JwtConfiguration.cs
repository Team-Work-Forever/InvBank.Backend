namespace InvBank.Backend.Infrastructure.Config;

public class JwtConfiguration
{
    public static string SectionName = "JWTConfiguration";

    public string Secret { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public string AccessTokenExpire { get; set; } = null!;
    public string RefreshTokenExpire { get; set; } = null!;

}