using InvBank.Backend.Domain.Entities;

namespace InvBank.Backend.Application.Common.Interfaces;

public interface IUserRepository
{

    Task<Auth?> GetUserAuth(string email);
    Task<Profile?> GetProfileByEmail(string email);
    Task<IEnumerable<Profile>> GetAllClients();
    Task CreateUser(Auth auth);
    Task AssociateAccount(Auth user, Account account);

}
