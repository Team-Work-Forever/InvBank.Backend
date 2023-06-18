using ErrorOr;
using MediatR;

namespace InvBank.Backend.Application.Authentication.Commands.RegisterCompany;

public record RegisterCompanyCommand(
    string Email,
    string Password,
    string Name,
    string Phone,
    string PostalCode,
    string Nif
) : IRequest<ErrorOr<string>>;