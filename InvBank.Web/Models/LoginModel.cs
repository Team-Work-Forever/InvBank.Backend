using System.ComponentModel.DataAnnotations;
using InvBank.Utils;

namespace InvBank.Models.auth;

public class LoginModel
{

    [Required, EmailAddress]
    public string Email { get; set; } = null!;

    [Required, DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    public IEnumerable<ValidationResult> Validate()
    {
        return ValidateUtil.Validate(this);
    }
}