using System.IdentityModel.Tokens.Jwt;

namespace InvBank.Web.Provider.Authorization;

public class JwtModifier : IJWTModifier
{

    public Dictionary<string, string> GetClaims(string token)
    {
        return new JwtSecurityTokenHandler().ReadJwtToken(token)
            .Claims
            .Select(claim => new KeyValuePair<string, string>(claim.Type, claim.Value))
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
    }
}