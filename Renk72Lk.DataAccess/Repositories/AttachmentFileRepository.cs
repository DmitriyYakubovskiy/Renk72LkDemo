using Renk72Lk.DataAccess.Contexts;
using Renk72Lk.DataAccess.Entities;

namespace Renk72Lk.DataAccess.Repositories;

public class AttachmentFileRepository : IAttachmentFileRepository
{
    private readonly ApplicationDbContext dbContext;

    public AttachmentFileRepository(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<AttachmentFileEntity> CreateAsync(AttachmentFileEntity entity)
    {
        entity.CreatedAt = DateTime.Now;
        entity.UpdatedAt = DateTime.Now;
        await dbContext.AttachmentFiles.AddAsync(entity);
        await dbContext.SaveChangesAsync();
        return entity;
    }

    public void Delete(int id)
    {
        var entity = GetById(id);
        if (entity == null) return;
        dbContext.AttachmentFiles.Remove(entity);
        dbContext.SaveChanges();
    }

    public bool FileExists(string filePath)
    {
        var entity = dbContext.AttachmentFiles.Where(x => x.FilePath == filePath).FirstOrDefault();
        return entity != null;
    }

    public AttachmentFileEntity[] GetAll()
    {
        return dbContext.AttachmentFiles.ToArray();
    }

    public AttachmentFileEntity GetById(int id)
    {
        return dbContext.AttachmentFiles.FirstOrDefault(x => x.Id == id)!;
    }

    public void Update(AttachmentFileEntity entity)
    {
        entity.UpdatedAt = DateTime.Now;
        dbContext.AttachmentFiles.Update(entity);
        dbContext.SaveChanges();
    }
}
