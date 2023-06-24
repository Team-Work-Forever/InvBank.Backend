using System.ComponentModel.DataAnnotations;
using InvBank.Utils;

namespace InvBank.Models;

public class Account
{
    [Required(ErrorMessage = "A conta é obrigatória."), StringLength(9, MinimumLength = 9, ErrorMessage = "A conta deve ter exatamente 9 dígitos.")]
    public string Iban { get; set; } = string.Empty;

    [Required(ErrorMessage = "A conta do banco é obrigatória."), StringLength(9, MinimumLength = 9, ErrorMessage = "A conta do banco deve ter exatamente 9 dígitos.")]
    public string AccountBank { get; set; } = string.Empty;

    public string BankName { get; set; } = string.Empty;

    public IEnumerable<ValidationResult> Validate()
    {
        return ValidateUtil.Validate(this);
    }

}