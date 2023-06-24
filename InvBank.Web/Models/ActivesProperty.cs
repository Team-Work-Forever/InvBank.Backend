using System.ComponentModel.DataAnnotations;
using InvBank.Utils;

namespace InvBank.Models;

public class ActivesProperty
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

    [Required(ErrorMessage = "A designação é obrigatório."), StringLength(100, ErrorMessage = "A designação não pode ter mais de 100 caracteres.")]
    public string Designation { get; set; } = string.Empty;

    [Required(ErrorMessage = "O código postal é obrigatório."), StringLength(8, MinimumLength = 8, ErrorMessage = "O código postal deve ter exatamente 8 dígitos ('-' incluído).")]
    public string PostalCode { get; set; } = string.Empty;

    [Required(ErrorMessage = "O valor da propriedade é obrigatório."), Range(0, 10000000000, ErrorMessage = "O valor da propriedade deve estar entre 0 e 10000000000.")]
    public decimal PropertyValue { get; set; } = 0;

    [Required(ErrorMessage = "O valor da renda é obrigatório."), Range(0, 10000000000, ErrorMessage = "O valor da renda deve estar entre 0 e 10000000000.")]
    public decimal RentValue { get; set; } = 0;

    [Required(ErrorMessage = "O valor mensal do condomínio é obrigatório."), Range(0, 10000000000, ErrorMessage = "O valor mensal do condomínio deve estar entre 0 e 10000000000.")]
    public decimal MonthValue { get; set; } = 0;

    [Required(ErrorMessage = "O valor anual estimado de outras despesas com o imóvel é obrigatório."), Range(0, 10000000000, ErrorMessage = "O valor anual estimado de outras despesas com o imóvel deve estar entre 0 e 10000000000.")]
    public decimal YearlyValue { get; set; } = 0;

    [Required(ErrorMessage = "A conta é obrigatória."), StringLength(30, MinimumLength = 30, ErrorMessage = "A conta deve ter exatamente 28 dígitos.")]
    public string Account { get; set; } = string.Empty;

    public IEnumerable<ValidationResult> Validate()
    {
        return ValidateUtil.Validate(this);
    }
}
