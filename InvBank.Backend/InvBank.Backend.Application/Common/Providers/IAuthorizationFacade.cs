using ErrorOr;
using InvBank.Backend.Domain.Entities;

namespace InvBank.Backend.Application.Common.Providers;

public interface IAuthorizationFacade
{
    Task<ErrorOr<Auth>> GetAuthenticatedUser();
}