using System;
using System.Collections.Generic;

namespace InvBank.Backend.Domain.Entities;

public partial class Auth
{
    public Guid Id { get; set; }

    public string Email { get; set; } = null!;

    public string UserPassword { get; set; } = null!;

    public bool IsEnable { get; set; }

    public int UserRole { get; set; }

    public virtual ICollection<AuthAccount> AuthAccounts { get; set; } = new List<AuthAccount>();

    public virtual Company Company { get; set; } = null!;

    public virtual Profile Profile { get; set; } = null!;
}
