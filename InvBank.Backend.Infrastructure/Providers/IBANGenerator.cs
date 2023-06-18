using System.Text;
using InvBank.Backend.Application.Common.Providers;

namespace InvBank.Backend.Infrastructure.Providers;

public class IBANGenerator : IIBANGenerator
{

    private readonly string portugalCountryCode = "PT50";
    private readonly string idBankCode = "0069";
    private readonly string pspIdentifier = "2635";
    private readonly string NIBControl = "72";

    public string GenerateIBAN()
    {
        return portugalCountryCode + " " + idBankCode + " " + pspIdentifier + " " + GenerateRandomNumber(11) + " " + NIBControl;
    }

    private string GenerateRandomNumber(int legth)
    {

        Random random = new Random();
        StringBuilder ranDigits = new StringBuilder();

        for (int i = 1; i < legth; i++)
        {
            ranDigits.Append(random.Next(0, 9).ToString());
        }

        return ranDigits.ToString();

    }

}