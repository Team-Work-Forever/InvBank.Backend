using System.Text.Json.Serialization;
using InvBank.Web.Contracts.Deposit;
using InvBank.Web.Contracts.PropertyAccount;

namespace InvBank.Web.Contracts.Account;

public class AccountResponse
{
    [JsonPropertyName("iban")]
    public string iban { get; set; } = null!;

    [JsonPropertyName("bank")]
    public string Bank { get; set; } = null!;

    [JsonPropertyName("amountValue")]
    public decimal AmountValue { get; set; }

    [JsonPropertyName("deposits")]
    public IEnumerable<DepositResponse> DepositResponse { get; set; } = null!;

    [JsonPropertyName("properties")]
    public IEnumerable<PropertyAccountResponse> Properties { get; set; } = null!;

}