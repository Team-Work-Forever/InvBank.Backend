using ErrorOr;

namespace InvBank.Web.Errors;

public static partial class Errors
{
    public static class Account
    {
        public static Error AccountNotFound => Error.NotFound(
                 "Account.AccountNotFound",
                 "Não foi encontrada nenhuma conta");
    }
}