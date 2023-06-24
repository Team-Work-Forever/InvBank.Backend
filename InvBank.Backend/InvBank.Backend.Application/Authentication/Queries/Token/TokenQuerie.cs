using ErrorOr;
using InvBank.Backend.Contracts.Authentication;
using MediatR;

namespace InvBank.Backend.Application.Authentication.Queries.TokenQuerie;

public record TokenQuerie
(
    string RefreshToken
) : IRequest<ErrorOr<AuthenticationResult>>;