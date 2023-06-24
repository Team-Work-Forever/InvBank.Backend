using InvBank.Backend.Contracts.Payment;
using InvBank.Backend.Contracts.Report;

namespace InvBank.Backend.Contracts.Report;

public record PayReportResponse
(
    IEnumerable<PayDepositResponse> PaymentDeposit,
    IEnumerable<PayPropertyResponse> PaymentProperty
);