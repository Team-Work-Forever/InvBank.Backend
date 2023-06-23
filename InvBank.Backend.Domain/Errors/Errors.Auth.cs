using ErrorOr;

namespace InvBank.Backend.Domain.Errors;

public static partial class Errors
{
    public static class Auth
    {
        public static Error DuplicatedEmail => Error.Conflict(
            "Auth.DuplicatedEmail",
            "Este email já se encontra em uso");

        public static Error AuthNotFoundById => Error.Conflict(
                "Auth.AuthNotFoundById",
                "Não existe nenhuma conta com esse id");
        public static Error IncorrentCredentials => Error.Conflict(
                "Auth.IncorrentCredentials",
                "O email ou password estão errados!");

        public static Error CannotGetEmail => Error.Unexpected(
                "Auth.CannotGetEmail",
                "Não é possivel obter o email");

        public static Error AuthorizationFailed => Error.Unexpected(
                "Auth.AuthorizationFailed",
                "Não estas autorizado a realizar esta ação");

    }
}