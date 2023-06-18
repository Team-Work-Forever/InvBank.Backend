using ErrorOr;

namespace InvBank.Backend.Domain.Errors;

public static partial class Errors
{
    public static class Auth
    {
        public static Error DuplicatedEmail => Error.Conflict(
            "Auth.DuplicatedEmail",
            "Este email já se encontra em uso");

        public static Error IncorrentCredentials => Error.Failure(
                "Auth.IncorrentCredentials",
                "O email ou password estão errados!");

    }
}