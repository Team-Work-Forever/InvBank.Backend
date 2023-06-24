using System.Globalization;
using InvBank.Backend.Application.Common.Providers;

namespace InvBank.Backend.Infrastructure;
public class DateTimeFormatter : IDateFormatter
{
    public DateOnly ConvertToDateTime(string date)
    {
        return DateOnly.Parse(date);
    }

    public string ConvertToString(DateOnly date)
    {
        return date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
    }
}
