using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using InvBank.Backend.Application.Common.Providers;
using InvBank.Backend.Infrastructure.Config;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace InvBank.Backend.Infrastructure.Providers;

public class JWTModifier : IJWTModifier
{
    private readonly JwtConfiguration _jwtConfiguration;

    public JWTModifier(IOptions<JwtConfiguration> jwtConfiguration)
    {
        _jwtConfiguration = jwtConfiguration.Value;
    }

    public string GenerateToken(Dictionary<string, string> claims, DateTime? expiresAt = null)
    {

        var signingTokens = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtConfiguration.Secret)),
                SecurityAlgorithms.HmacSha256
        );

        var securityToken = new JwtSecurityToken
        (
            issuer: _jwtConfiguration.Issuer,
            expires: expiresAt ?? DateTime.UtcNow.AddHours(2),
            claims: claims.Select(prop => new Claim(prop.Key, prop.Value)),
            signingCredentials: signingTokens,
            audience: _jwtConfiguration.Audience
        );

        return new JwtSecurityTokenHandler().WriteToken(securityToken);

    }

    public Dictionary<string, string> GetClaims(string token)
    {
        return new JwtSecurityTokenHandler().ReadJwtToken(token)
            .Claims
            .Select(claim => new KeyValuePair<string, string>(claim.Type, claim.Value))
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
    }
}