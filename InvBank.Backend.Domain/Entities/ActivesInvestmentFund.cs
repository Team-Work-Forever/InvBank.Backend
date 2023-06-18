using System;
using System.Collections.Generic;

namespace InvBank.Backend.Domain.Entities;

public partial class ActivesInvestmentFund
{
    public Guid Id { get; set; }

    public string InvestName { get; set; } = null!;

    public DateOnly InitialDate { get; set; }

    public int Duration { get; set; }

    public decimal InvestValue { get; set; }

    public decimal TaxPercent { get; set; }

    public DateOnly CreatedAt { get; set; }

    public DateOnly UpdatedAt { get; set; }

    public DateOnly? DeletedAt { get; set; }

    public virtual ICollection<AccountInv> AccountInvs { get; set; } = new List<AccountInv>();

    public virtual ICollection<AtiveStateInvestmentFund> AtiveStateInvestmentFunds { get; set; } = new List<AtiveStateInvestmentFund>();
}
