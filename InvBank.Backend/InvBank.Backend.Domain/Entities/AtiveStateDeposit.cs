

namespace InvBank.Backend.Domain.Entities;

public partial class AtiveStateDeposit
{
    public Guid Id { get; set; }

    public Guid AtiveId { get; set; }

    public bool IsActive { get; set; }

    public DateOnly? InitDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public virtual ActivesDepositAccount Ative { get; set; } = null!;
}
