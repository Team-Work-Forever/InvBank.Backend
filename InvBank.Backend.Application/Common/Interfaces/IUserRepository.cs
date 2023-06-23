using InvBank.Backend.Domain.Entities;

namespace InvBank.Backend.Application.Common.Interfaces;

public interface IUserRepository
{

    Task<Auth?> GetUserAuth(string email);
    Task<Auth?> GetUserAuth(Guid id);
    Task<Profile?> GetProfileByEmail(string email);
    Task<IEnumerable<Auth>> GetAllUsers();
    Task<IEnumerable<Profile>> GetAllClients();
    Task CreateUser(Auth auth);
    Task AssociateAccount(Auth user, Account account);
    Task<Auth> UpdateAuth(Auth auth);
    Task DeleteAuth(Auth auth);

}
