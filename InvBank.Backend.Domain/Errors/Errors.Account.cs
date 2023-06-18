using ErrorOr;

namespace InvBank.Backend.Domain.Errors;

public static partial class Errors
{
    public static class Account
    {
        public static Error AccountNotFound => Error.NotFound(
            "Auth.AccountNotFound",
            "Não foi encontrada nenhuma conta");
    }
}