

namespace InvBank.Backend.Domain.Entities;

public partial class Postalcode
{
    public string PostalCode1 { get; set; } = null!;

    public string Street { get; set; } = null!;

    public int Port { get; set; }

    public string Locality { get; set; } = null!;

    public DateOnly CreatedAt { get; set; }

    public DateOnly UpdatedAt { get; set; }

    public DateOnly? DeletedAt { get; set; }
}
