using ErrorOr;

namespace InvBank.Backend.Domain.Errors;

public static partial class Errors
{
    public class PropertyAccount
    {
        public static Error PropertyAccountNotFound => Error.NotFound(
                    "PropertyAccount.PropertyAccountNotFound",
                    "Não foi encontrado nenhuma conta de ativos moveis");
        
        public static Error PropertyAmountGreater => Error.Conflict(
                    "PropertyAccount.PropertyAmountGreater",
                    "Não é possível liquidar um valor maior do que o esperado");
    }
}