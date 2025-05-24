using Renk72Lk.DataAccess.Contexts;
using Renk72Lk.DataAccess.Entities;

namespace Renk72Lk.DataAccess.Repositories;

public class AttachmentsStageRepository : IAttachmentsStageRepository
{
    private readonly ApplicationDbContext dbContext;

    public AttachmentsStageRepository(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<int> CreateAsync(AttachmentsStageEntity entity)
    {
        entity.CreatedAt = DateTime.Now;
        entity.UpdatedAt = DateTime.Now;
        await dbContext.AttachmentsStages.AddAsync(entity);
        await dbContext.SaveChangesAsync();
        return entity.Id;
    }

    public void Delete(int id)
    {
        var entity = GetById(id);
        if (entity == null) return;
        dbContext.AttachmentsStages.Remove(entity);
        dbContext.SaveChanges();
    }

    public AttachmentsStageEntity[] GetAll()
    {
        return dbContext.AttachmentsStages.ToArray();
    }

    public AttachmentsStageEntity GetById(int id)
    {
        return dbContext.AttachmentsStages.FirstOrDefault(x => x.Id == id)!;
    }

    public void Update(AttachmentsStageEntity entity)
    {
        entity.UpdatedAt = DateTime.Now;
        dbContext.AttachmentsStages.Update(entity);
        dbContext.SaveChanges();
    }
}
