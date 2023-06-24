namespace InvBank.Backend.Infrastructure.Persistence;

public class BaseRepository
{

    protected readonly InvBankDbContext _dbContext;

    public BaseRepository(InvBankDbContext dbContext)
    {
        _dbContext = dbContext;
    }

}