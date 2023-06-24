using System.Globalization;

namespace InvBank.Backend.Application.Validators.Common;

public static class ValidateUtils
{

    public static bool MustBeDate(string dateTime)
    {
        return DateTime.TryParseExact(dateTime,
                                      "dd/MM/yyyy",
                                      CultureInfo.InvariantCulture,
                                      DateTimeStyles.None,
                                      out var date);
    }

}