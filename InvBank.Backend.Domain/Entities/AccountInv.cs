using System;
using System.Collections.Generic;

namespace InvBank.Backend.Domain.Entities;

public partial class AccountInv
{
    public Guid Id { get; set; }

    public string Account { get; set; } = null!;

    public decimal AccountValue { get; set; }

    public DateOnly JoinAt { get; set; }

    public virtual Account AccountNavigation { get; set; } = null!;

    public virtual ActivesInvestmentFund IdNavigation { get; set; } = null!;
}
