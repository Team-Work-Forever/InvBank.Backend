using System;
using System.Collections.Generic;

namespace InvBank.Backend.Domain.Entities;

public partial class Profile
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateOnly BirthDate { get; set; }

    public string Nif { get; set; } = null!;

    public string Cc { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string PostalCode { get; set; } = null!;

    public DateOnly CreatedAt { get; set; }

    public DateOnly UpdatedAt { get; set; }

    public DateOnly? DeletedAt { get; set; }

    public virtual Auth IdNavigation { get; set; } = null!;
}
