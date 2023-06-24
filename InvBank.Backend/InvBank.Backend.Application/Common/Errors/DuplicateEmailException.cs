namespace InvBank.Backend.Application.Common.Errors;

public class DuplicateEmailException : BaseException
{
    public DuplicateEmailException(string? message) : base(message, 400)
    {
    }
}