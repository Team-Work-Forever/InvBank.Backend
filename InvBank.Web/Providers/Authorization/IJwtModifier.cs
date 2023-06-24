namespace InvBank.Web.Provider.Authorization;

public interface IJWTModifier
{
    Dictionary<string, string> GetClaims(string token);

}