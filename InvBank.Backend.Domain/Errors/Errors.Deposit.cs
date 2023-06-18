using ErrorOr;

namespace InvBank.Backend.Domain.Errors;

public static partial class Errors
{
    public class Deposit
    {
        public static Error DepositNotFound => Error.NotFound(
                    "Deposit.DepositNotFound",
                    "Não foi encontrado nenhuma conta de deposito");
    }
}