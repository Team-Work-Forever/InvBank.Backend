using System;
using System.Collections.Generic;

namespace InvBank.Backend.Domain.Entities;

public partial class AtiveStateInvestmentFund
{
    public Guid Id { get; set; }

    public Guid AtiveId { get; set; }

    public bool IsActive { get; set; }

    public DateOnly InitDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public virtual ActivesInvestmentFund Ative { get; set; } = null!;
}
