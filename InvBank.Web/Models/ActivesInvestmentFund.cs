using System.ComponentModel.DataAnnotations;
using InvBank.Utils;

namespace InvBank.Models;
public class ActivesInvestmentFund
{
    public int Id { get; set; } = 0;

    [Required(ErrorMessage = "O nome é obrigatório."), StringLength(100, ErrorMessage = "O nome não pode ter mais de 100 caracteres.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "A data inicial é obrigatória.")]
    public DateTime InitialDate { get; set; } = DateTime.UtcNow;

    [Required(ErrorMessage = "A duração é obrigatória."), Range(0, 1000, ErrorMessage = "A duração deve estar entre 0 e 1000.")]
    public int Duration { get; set; } = 0;

    [Required(ErrorMessage = "A percentagem de imposto é obrigatória."), Range(0, 300, ErrorMessage = "A percentagem de imposto deve estar entre 0 e 300.")]
    public decimal TaxPercent { get; set; } = 0;

    public IEnumerable<ValidationResult> Validate()
    {
        return ValidateUtil.Validate(this);
    }
}
