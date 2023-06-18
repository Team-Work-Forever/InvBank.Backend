

namespace InvBank.Backend.Domain.Entities;

public partial class AuthAccount
{
    public Guid Auth { get; set; }

    public string Account { get; set; } = null!;

    public DateOnly JoinAt { get; set; }

    public virtual Account AccountNavigation { get; set; } = null!;

    public virtual Auth AuthNavigation { get; set; } = null!;
}
