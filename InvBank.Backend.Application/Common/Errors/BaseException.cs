namespace InvBank.Backend.Application.Common.Errors;

public class BaseException : Exception
{

    public int HttpStatusCode { get; set; }

    public BaseException(string? message, int status) : base(message)
    {
        HttpStatusCode = status;
    }

}