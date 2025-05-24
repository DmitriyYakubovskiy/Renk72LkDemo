using Microsoft.EntityFrameworkCore;
using Renk72Lk.DataAccess.Contexts;
using Renk72Lk.DataAccess.Entities;

namespace Renk72Lk.DataAccess.Repositories;

public class BidTechnicalSpecificationsRepository : IBidTechnicalSpecificationsRepository
{
    private readonly ApplicationDbContext dbContext;

    public BidTechnicalSpecificationsRepository(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task CreateAsync(BidTechnicalSpecificationsEntity entity)
    {
        entity.CreatedAt = DateTime.Now;
        entity.UpdatedAt = DateTime.Now;    
        await dbContext.TechnicalSpecifications.AddAsync(entity);
        dbContext.SaveChanges();
    }

    public void Delete(int id)
    {
        var entity = GetById(id);
        if (entity == null) return;
        dbContext.TechnicalSpecifications.Remove(entity);
        dbContext.SaveChanges();
    }

    public BidTechnicalSpecificationsEntity[] GetAll()
    {
        return dbContext.TechnicalSpecifications.ToArray();
    }

    public BidTechnicalSpecificationsEntity GetOne(int userId, int bidId)
    {
        return dbContext.TechnicalSpecifications.Where(e => e.UserId == userId).Where(x => x.BidId == bidId)
            .Include(x => x.Stages).Include(x=>x.Points).OrderByDescending(x => x.UpdatedAt).FirstOrDefault()!;
    }

    public BidTechnicalSpecificationsEntity GetById(int id)
    {
        return dbContext.TechnicalSpecifications.FirstOrDefault(x => x.Id == id)!;
    }

    public void Update(BidTechnicalSpecificationsEntity entity)
    {
        entity.UpdatedAt = DateTime.Now;
        dbContext.Update(entity);
        dbContext.SaveChanges();
    }
}
