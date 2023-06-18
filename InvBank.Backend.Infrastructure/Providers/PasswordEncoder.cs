using InvBank.Backend.Application.Common.Providers;
using BC = BCrypt.Net.BCrypt;

namespace InvBank.Backend.Infrastructure.Providers;

public class PasswordEncoder : IPasswordEncoder
{

    private readonly int ENCRIPT_COST = 12;

    public string Encode(string password)
    {
        return BC.HashPassword(password, ENCRIPT_COST);
    }

    public bool VerifyHash(string encodedPassword, string originalPassword)
    {
        return BC.Verify(originalPassword, encodedPassword);
    }
}