using Microsoft.EntityFrameworkCore;
using Renk72Lk.DataAccess.Contexts;
using Renk72Lk.DataAccess.Entities;

namespace Renk72Lk.DataAccess.Repositories;

public class BidPersonalInfoRepository : IBidPersonalInfoRepository
{
    private readonly ApplicationDbContext dbContext;

    public BidPersonalInfoRepository(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task CreateAsync(BidPersonalInfoEntity entity)
    {
        entity.CreatedAt = DateTime.Now;
        entity.UpdatedAt = DateTime.Now;
        await dbContext.PersonalInfos.AddAsync(entity);
        dbContext.SaveChanges();
    }

    public void Delete(int id)
    {
        var entity = GetById(id);
        if (entity == null) return;
        dbContext.PersonalInfos.Remove(entity);
        dbContext.SaveChanges();
    }

    public BidPersonalInfoEntity[] GetAll()
    {
        return dbContext.PersonalInfos.ToArray();
    }

    public BidPersonalInfoEntity GetOne(int userId, int bidId)
    {
        return dbContext.PersonalInfos.Where(e => e.UserId == userId).Where(x => x.BidId == bidId)
            .Include(x=>x.ActualAddress).Include(x => x.RegistrationAddress)
            .OrderByDescending(x => x.UpdatedAt).FirstOrDefault()!;
    }

    public BidPersonalInfoEntity GetById(int id)
    {
        return dbContext.PersonalInfos.FirstOrDefault(x => x.Id == id)!;
    }

    public void Update(BidPersonalInfoEntity entity)
    {
        entity.UpdatedAt = DateTime.Now;
        dbContext.Update(entity);
        dbContext.SaveChanges();
    }

    public BidPersonalInfoEntity GetConsumer(int userId)
    {
        return dbContext.PersonalInfos.Where(e => e.UserId == userId).Where(x => x.Name != null)
            .OrderByDescending(x => x.UpdatedAt).FirstOrDefault()!;
    }
}
