using ErrorOr;

namespace InvBank.Backend.Domain.Errors;

public static partial class Errors
{
    public static class Fund
    {
        public static Error FundNotFound => Error.NotFound(
            "Auth.FundNotFound",
            "Não foi encontrada nenhum fundo de investimento");
            
        public static Error AlreadyAssociatedAccount => Error.NotFound(
            "Auth.AlreadyAssociatedAccount",
            "Não é possível associar duas vezes ou mais a mesma conta");

        public static Error FundIsNotAssociated => Error.NotFound(
            "Auth.FundIsNotAssociated",
            "Este fundo não está disponível para investir");

        public static Error FundAmountGreater => Error.Conflict(
                "Fund.FundAmountGreater",
                "Não é possível liquidar um valor maior do que a conta pode fornecer");

        public static Error FundAmountMinor => Error.Conflict(
                "Fund.FundAmountMinor",
                "Não é possível liquidar um valor menor do que o esperado");
    }
}