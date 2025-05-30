using Renk72Lk.DataAccess.Entities;

namespace Renk72Lk.DataAccess.Repositories;

public interface IAttachmentFileRepository
{
    Task<AttachmentFileEntity> CreateAsync(AttachmentFileEntity entity);
    bool FileExists(string filePath);
    void Delete(int id);
    AttachmentFileEntity[] GetAll();
    AttachmentFileEntity GetById(int id);
    void Update(AttachmentFileEntity entity);
}