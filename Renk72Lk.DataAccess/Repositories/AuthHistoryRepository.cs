using Renk72Lk.DataAccess.Contexts;
using Renk72Lk.DataAccess.Entities;

namespace Renk72Lk.DataAccess.Repositories;

public class AuthHistoryRepository : IAuthHistoryRepository
{
    private readonly ApplicationDbContext dbContext;

    public AuthHistoryRepository(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task CreateAsync(AuthHistoryEntity entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        entity.UpdatedAt = DateTime.UtcNow;
        await dbContext.AuthHistory.AddAsync(entity);
        dbContext.SaveChanges();
    }

    public void Delete(int id)
    {
        var entity = GetById(id);
        if (entity == null) return;
        dbContext.AuthHistory.Remove(entity);
        dbContext.SaveChanges();
    }

    public AuthHistoryEntity[] GetAll()
    {
        return dbContext.AuthHistory.ToArray();
    }

    public AuthHistoryEntity[] GetAll(int userId, int lastCount=-1)
    {
        var thirtyDaysAgo = DateTime.UtcNow.AddDays(-30);

        if (lastCount < 0)
        {
            return dbContext.AuthHistory
                .Where(e => e.UserId == userId)
                .ToArray();
        }
        else
        {
            var result = dbContext.AuthHistory
                .Where(e => e.UserId == userId && e.CreatedAt >= thirtyDaysAgo)
                .OrderByDescending(e => e.CreatedAt)
                .Take(lastCount)
                .ToArray()
                .GroupBy(e => e.LoginIp)
                .Select(g => g.OrderByDescending(e => e.CreatedAt).FirstOrDefault())
                .Take(lastCount)
                .ToArray();
            return result!;
        }
    }

    public AuthHistoryEntity GetById(int id)
    {
        return dbContext.AuthHistory.FirstOrDefault(x => x.Id == id)!;
    }

    public void Update(AuthHistoryEntity entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        dbContext.Update(entity);
        dbContext.SaveChanges();
    }
}
