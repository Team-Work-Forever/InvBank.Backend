using ErrorOr;

namespace InvBank.Backend.Domain.Errors;

public static partial class Errors
{
    public class Bank
    {
        public static Error BankNotFound => Error.NotFound(
            "Bank.BankNotFound",
            "Não foi encontrado nenhum banco");
    }
}