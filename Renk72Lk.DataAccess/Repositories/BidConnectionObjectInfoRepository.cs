using Microsoft.EntityFrameworkCore;
using Renk72Lk.DataAccess.Contexts;
using Renk72Lk.DataAccess.Entities;

namespace Renk72Lk.DataAccess.Repositories;

public class BidConnectionObjectInfoRepository : IBidConnectionObjectInfoRepository
{
    private readonly ApplicationDbContext dbContext;

    public BidConnectionObjectInfoRepository(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task CreateAsync(BidConnectionObjectInfoEntity entity)
    {
        entity.CreatedAt = DateTime.Now;
        entity.UpdatedAt = DateTime.Now;
        await dbContext.ConnectionObjectInfos.AddAsync(entity);
        dbContext.SaveChanges();
    }

    public void Delete(int id)
    {
        var entity = GetById(id);
        if (entity == null) return;
        dbContext.ConnectionObjectInfos.Remove(entity);
        dbContext.SaveChanges();
    }

    public BidConnectionObjectInfoEntity[] GetAll()
    {
        return dbContext.ConnectionObjectInfos.ToArray();
    }

    public BidConnectionObjectInfoEntity GetOne(int userId, int bidId)
    {
        return dbContext.ConnectionObjectInfos.Where(e => e.UserId == userId).Where(x => x.BidId == bidId)
            .OrderByDescending(x => x.UpdatedAt).FirstOrDefault()!;
    }

    public BidConnectionObjectInfoEntity GetById(int id)
    {
        return dbContext.ConnectionObjectInfos.FirstOrDefault(x => x.Id == id)!;
    }

    public void Update(BidConnectionObjectInfoEntity entity)
    {
        entity.UpdatedAt = DateTime.Now;
        dbContext.Update(entity);
        dbContext.SaveChanges();
    }
}
