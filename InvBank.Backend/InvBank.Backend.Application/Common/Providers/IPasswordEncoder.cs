namespace InvBank.Backend.Application.Common.Providers;

public interface IPasswordEncoder
{
    string Encode(string password);
    bool VerifyHash(string encodedPassword, string originalPassword);
}