using System.ComponentModel.DataAnnotations;
using InvBank.Utils;

namespace InvBank.Models.auth;

public class RegisterCompanyModel
{

    [Required(ErrorMessage = "O email é obrigatório."), EmailAddress]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "A palavra-passe é obrigatória."), StringLength(100, ErrorMessage = "A palavra-passe dever ter menos de 100 caracteres.", MinimumLength = 6), DataType(DataType.Password), Display(Name = "Password")]
    public string Password { get; set; } = null!;

    [Required(ErrorMessage = "A confirmação é obrigatória."), DataType(DataType.Password), Compare("Password", ErrorMessage = "As palavras-passes não coincidem!")]
    public string ConfirmPassword { get; set; } = null!;

    [Required(ErrorMessage = "O nome é obrigatório."), StringLength(100, ErrorMessage = "O  dever ter menos de 100 caracteres.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "O NIF é obrigatório."), StringLength(100, MinimumLength = 8, ErrorMessage = "O  dever ter menos de 9 caracteres.")]
    public string Nif { get; set; } = string.Empty;

    [Required(ErrorMessage = "O Telefone é obrigatório.")]
    public string Phone { get; set; } = string.Empty;

    [Required(ErrorMessage = "O código postal é obrigatório."), StringLength(8, MinimumLength = 8, ErrorMessage = "O código postal deve ter exatamente 8 dígitos ('-' incluído).")]
    public string PostalCode { get; set; } = string.Empty;

    public IEnumerable<ValidationResult> Validate()
    {
        return ValidateUtil.Validate(this);
    }
}