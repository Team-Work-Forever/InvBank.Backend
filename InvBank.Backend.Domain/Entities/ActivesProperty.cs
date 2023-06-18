

namespace InvBank.Backend.Domain.Entities;

public partial class ActivesProperty
{
    public Guid Id { get; set; }

    public string PropertyName { get; set; } = null!;

    public DateOnly InitialDate { get; set; }

    public int Duration { get; set; }

    public decimal TaxPercent { get; set; }

    public string Designation { get; set; } = null!;

    public string PostalCode { get; set; } = null!;

    public decimal PropertyValue { get; set; }

    public decimal RentValue { get; set; }

    public decimal MonthValue { get; set; }

    public decimal YearlyValue { get; set; }

    public string Account { get; set; } = null!;

    public DateOnly CreatedAt { get; set; }

    public DateOnly UpdatedAt { get; set; }

    public DateOnly? DeletedAt { get; set; }

    public virtual Account? AccountNavigation { get; set; }

    public virtual ICollection<AtiveStateProperty> AtiveStateProperties { get; set; } = new List<AtiveStateProperty>();

    public virtual ICollection<PaymentProperty> PaymentProperties { get; set; } = new List<PaymentProperty>();
}
