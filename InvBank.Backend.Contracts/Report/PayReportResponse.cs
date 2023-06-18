using InvBank.Backend.Contracts.Payment;

namespace InvBank.Backend.Contracts.Report;

public record PayReportResponse
(
    IEnumerable<PayDepositResponse> PaymentDeposit,
    IEnumerable<PayPropertyResponse> PaymentProperty
);