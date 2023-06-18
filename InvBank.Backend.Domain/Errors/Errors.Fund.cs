using ErrorOr;

namespace InvBank.Backend.Domain.Errors;

public static partial class Errors
{
    public class Fund
    {
        public static Error FundNotFound => Error.NotFound(
                    "Fund.FundNotFound",
                    "Não foi encontrado nenhuma conta de investimento de fundos");
    }
}