namespace InvBank.Backend.Application.Common.Providers;

public interface IDateFormatter
{
    DateOnly ConvertToDateTime(string date);
    string ConvertToString(DateOnly date);

}