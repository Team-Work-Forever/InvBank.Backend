

namespace InvBank.Backend.Domain.Entities;

public partial class ActivesDepositAccount
{
    public Guid Id { get; set; }

    public string DepositName { get; set; } = null!;

    public DateOnly InitialDate { get; set; }

    public int Duration { get; set; }

    public decimal TaxPercent { get; set; }

    public decimal DepositValue { get; set; }

    public decimal YearlyTax { get; set; }

    public string Account { get; set; } = null!;

    public DateOnly CreatedAt { get; set; }

    public DateOnly UpdatedAt { get; set; }

    public DateOnly? DeletedAt { get; set; }

    public virtual Account? AccountNavigation { get; set; }

    public virtual ICollection<AtiveStateDeposit> AtiveStateDeposits { get; set; } = new List<AtiveStateDeposit>();

    public virtual ICollection<PaymentDeposit> PaymentDeposits { get; set; } = new List<PaymentDeposit>();
}
