using ErrorOr;

namespace InvBank.Web.Errors;

public static partial class Errors
{
    public static class Auth
    {
        public static Error RequestFailed => Error.Failure(
                 "Auth.RequestFailed",
                 "Ocorreu um erro");
    }
}