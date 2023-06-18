using InvBank.Backend.Domain.Entities;

namespace InvBank.Backend.Application.Common.Contracts;

public record PayReportResult
(
    IEnumerable<PaymentDeposit> PaymentDeposit,
    IEnumerable<PaymentProperty> PaymentProperty
);