

namespace InvBank.Backend.Domain.Entities;

public partial class PaymentInvestFund
{
    public Guid Id { get; set; }

    public Guid AtiveId { get; set; }

    public DateOnly? PaymentDate { get; set; }

    public decimal? Amount { get; set; }

    public virtual ActivesInvestmentFund Ative { get; set; } = null!;
}
