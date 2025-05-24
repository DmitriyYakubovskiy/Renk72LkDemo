using Renk72Lk.DataAccess.Contexts;
using Renk72Lk.DataAccess.Entities;

namespace Renk72Lk.DataAccess.Repositories;

public class BidAttachmentsRepository : IBidAttachmentsRepository
{
    private readonly ApplicationDbContext dbContext;

    public BidAttachmentsRepository(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task CreateAsync(BidAttachmentsEntity entity)
    {
        entity.CreatedAt = DateTime.Now;
        entity.UpdatedAt = DateTime.Now;
        await dbContext.Attachments.AddAsync(entity);
        dbContext.SaveChanges();
    }

    public void Delete(int id)
    {
        var entity = GetById(id);
        if (entity == null) return;
        dbContext.Attachments.Remove(entity);
        dbContext.SaveChanges();
    }

    public BidAttachmentsEntity[] GetAll()
    {
        return dbContext.Attachments.ToArray();
    }

    public BidAttachmentsEntity GetOne(int userId, int bidId)
    {
        return dbContext.Attachments.Where(e => e.UserId == userId).Where(x => x.BidId == bidId)
            .OrderByDescending(x => x.UpdatedAt).FirstOrDefault()!;
    }

    public BidAttachmentsEntity GetById(int id)
    {
        return dbContext.Attachments.FirstOrDefault(x => x.Id == id)!;
    }

    public void Update(BidAttachmentsEntity entity)
    {
        entity.UpdatedAt = DateTime.Now;
        dbContext.Update(entity);
        dbContext.SaveChanges();
    }
}
