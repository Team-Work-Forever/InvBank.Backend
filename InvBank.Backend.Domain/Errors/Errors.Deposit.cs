using ErrorOr;

namespace InvBank.Backend.Domain.Errors;

public static partial class Errors
{
    public class Deposit
    {
        public static Error DepositNotFound => Error.NotFound(
                    "Deposit.DepositNotFound",
                    "Não foi encontrado nenhuma conta de deposito");

        public static Error DepositAmountGreater => Error.Conflict(
                    "Deposit.DepositAmountGreater",
                    "Não é possível liquidar um valor maior do que a conta pode fornecer");

        public static Error DepositAmountMinor => Error.Conflict(
                    "Deposit.DepositAmountMinor",
                    "Não é possível liquidar um valor menor do que o esperado");
    }
}