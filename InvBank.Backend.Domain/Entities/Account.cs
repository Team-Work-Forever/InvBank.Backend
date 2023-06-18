namespace InvBank.Backend.Domain.Entities;

public partial class Account
{
    public string Iban { get; set; } = null!;

    public string Bank { get; set; } = null!;

    public DateOnly CreatedAt { get; set; }

    public DateOnly UpdatedAt { get; set; }

    public DateOnly? DeletedAt { get; set; }

    public virtual ICollection<AccountInv> AccountInvs { get; set; } = new List<AccountInv>();

    public virtual ICollection<ActivesDepositAccount> ActivesDepositAccounts { get; set; } = new List<ActivesDepositAccount>();

    public virtual ICollection<ActivesProperty> ActivesProperties { get; set; } = new List<ActivesProperty>();

    public virtual ICollection<AuthAccount> AuthAccounts { get; set; } = new List<AuthAccount>();

    public virtual Bank? BankNavigation { get; set; }
}
