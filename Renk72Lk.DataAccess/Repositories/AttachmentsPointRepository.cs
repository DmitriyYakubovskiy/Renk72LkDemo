using Renk72Lk.DataAccess.Contexts;
using Renk72Lk.DataAccess.Entities;

namespace Renk72Lk.DataAccess.Repositories;

public class AttachmentsPointRepository : IAttachmentsPointRepository
{
    private readonly ApplicationDbContext dbContext;

    public AttachmentsPointRepository(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<int> CreateAsync(AttachmentsPointEntity entity)
    {
        entity.CreatedAt = DateTime.Now;
        entity.UpdatedAt = DateTime.Now;
        await dbContext.AttachmentsPoints.AddAsync(entity);
        await dbContext.SaveChangesAsync();
        return entity.Id;
    }

    public void Delete(int id)
    {
        var entity = GetById(id);
        if (entity == null) return;
        dbContext.AttachmentsPoints.Remove(entity);
        dbContext.SaveChanges();
    }

    public AttachmentsPointEntity[] GetAll()
    {
        return dbContext.AttachmentsPoints.ToArray();
    }

    public AttachmentsPointEntity GetById(int id)
    {
        return dbContext.AttachmentsPoints.FirstOrDefault(x => x.Id == id)!;
    }

    public void Update(AttachmentsPointEntity entity)
    {
        entity.UpdatedAt = DateTime.Now;
        dbContext.AttachmentsPoints.Update(entity);
        dbContext.SaveChanges();
    }
}
