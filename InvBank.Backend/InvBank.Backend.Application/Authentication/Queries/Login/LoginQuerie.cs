using ErrorOr;
using InvBank.Backend.Contracts.Authentication;
using MediatR;

namespace InvBank.Backend.Application.Authentication.Queries.Login;

public record LoginQuerie
(
    string Email,
    string Password
) : IRequest<ErrorOr<AuthenticationResult>>;