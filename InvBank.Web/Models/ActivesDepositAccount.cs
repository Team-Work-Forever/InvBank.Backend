using System.ComponentModel.DataAnnotations;
using InvBank.Utils;

namespace InvBank.Models;

public class ActivesDepositAccount
{

    public Guid Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório."), StringLength(100, ErrorMessage = "O nome não pode ter mais de 100 caracteres.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "A data inicial é obrigatória.")]
    public DateTime InitialDate { get; set; } = DateTime.UtcNow;

    [Required(ErrorMessage = "A duração é obrigatória."), Range(0, 1000, ErrorMessage = "A duração deve estar entre 0 e 1000.")]
    public int Duration { get; set; } = 0;

    [Required(ErrorMessage = "A percentagem de imposto é obrigatória."), Range(0, 300, ErrorMessage = "A percentagem de imposto deve estar entre 0 e 300.")]
    public decimal TaxPercent { get; set; } = 0;

    [Required(ErrorMessage = "O valor é obrigatório."), Range(0, 10000000000, ErrorMessage = "O valor deve estar entre 0 e 10000000000.")]
    public decimal Value { get; set; } = 0;

    [Required(ErrorMessage = "A percentagem de imposto anual é obrigatória."), Range(0, 300, ErrorMessage = "A taxa percentagem de imposto deve estar entre 0 e 300.")]
    public decimal YearlyTax { get; set; } = 0;

    [Required(ErrorMessage = "A conta é obrigatória."), StringLength(30, MinimumLength = 30, ErrorMessage = "A conta deve ter exatamente 28 dígitos.")]
    public string Account { get; set; } = string.Empty;

    public IEnumerable<ValidationResult> Validate()
    {
        return ValidateUtil.Validate(this);
    }
}
