using ErrorOr;
using InvBank.Backend.Contracts.Authentication;
using MediatR;

namespace InvBank.Backend.Application.Authentication.Commands.RegisterClient;

public record RegisterClientCommand
(
    string Email,
    string Password,
    string FirstName,
    string LastName,
    string BirthDate,
    string Nif,
    string Cc,
    string Phone,
    string PostalCode
) : IRequest<ErrorOr<AuthenticationResult>>;
