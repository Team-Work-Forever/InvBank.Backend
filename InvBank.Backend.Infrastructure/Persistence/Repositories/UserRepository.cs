﻿using InvBank.Backend.Application.Common.Interfaces;
using InvBank.Backend.Domain.Entities;
using InvBank.Backend.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InvBank.Backend.Infrastructure;

public class UserRepository : IUserRepository
{
    private readonly InvBankDbContext _dbContext;

    public UserRepository(InvBankDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateUser(Auth auth)
    {
        _dbContext.Auths.Add(auth);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Profile?> GetProfileByEmail(string email)
    {
        return await _dbContext.Profiles.Where(prof => prof.IdNavigation.Email == email).FirstAsync();
    }

    public async Task<Auth?> GetUserAuth(string email)
    {
        return await _dbContext.Auths.Where(auth => auth.Email == email).FirstOrDefaultAsync();
    }

    public async Task AssociateAccount(Auth user, Account account)
    {

        AuthAccount authAccount = new AuthAccount
        {
            Account = account.Iban,
            AccountNavigation = account,
            Auth = user.Id,
            AuthNavigation = user
        };

        await _dbContext.AuthAccounts.AddAsync(authAccount);
        await _dbContext.SaveChangesAsync();

    }

    public async Task<IEnumerable<Profile>> GetAllClients()
    {
        return await _dbContext
            .Profiles
            .Where(prof => prof.IdNavigation.UserRole == 0)
            .Include(prof => prof.IdNavigation)
            .ToListAsync();
    }

    public async Task<Auth?> GetUserAuth(Guid id)
    {
        return await _dbContext
            .Auths
            .Where(auth => auth.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<Auth> UpdateAuth(Auth auth)
    {
        _dbContext.Update<Auth>(auth);
        await _dbContext.SaveChangesAsync();

        return auth;
    }

    public async Task DeleteAuth(Auth auth)
    {
        var authFind = await _dbContext.Auths
            .Where(ada => ada.Id == auth.Id)
            .Include(a => a.Profile)
            .FirstAsync();

        if (authFind.Profile is null)
        {
            return;
        }

        authFind.Profile.DeletedAt = DateOnly.FromDateTime(DateTime.Now);

        await UpdateAuth(authFind);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Auth>> GetAllUsers()
    {
        return await _dbContext
            .Auths
            .Include(a => a.Profile)
            .ToListAsync();
    }
}
