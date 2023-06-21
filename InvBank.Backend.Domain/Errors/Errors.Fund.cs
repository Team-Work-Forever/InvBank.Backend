using ErrorOr;

namespace InvBank.Backend.Domain.Errors;

public static partial class Errors
{
    public static class Fund
    {
        public static Error FundNotFound => Error.NotFound(
            "Auth.FundNotFound",
            "Não foi encontrada nenhum fundo de investimento");
    }
}