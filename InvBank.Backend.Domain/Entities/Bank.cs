

namespace InvBank.Backend.Domain.Entities;

public partial class Bank
{
    public string Iban { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string PostalCode { get; set; } = null!;

    public DateOnly CreatedAt { get; set; }

    public DateOnly UpdatedAt { get; set; }

    public DateOnly? DeletedAt { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}
