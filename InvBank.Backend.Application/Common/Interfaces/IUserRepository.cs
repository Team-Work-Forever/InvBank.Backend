using InvBank.Backend.Domain.Entities;

namespace InvBank.Backend.Application.Common.Interfaces;

public interface IUserRepository
{

    Task<Auth?> GetUserAuth(string email);
    Task<Auth?> GetProfileByEmail(string email);
    Task CreateUser(Auth auth);
    Task AssociateAccount(Auth user, Account account);
    Task<IEnumerable<Auth>> GetAllAuths();

}
