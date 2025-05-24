using Renk72Lk.DataAccess.Entities;

namespace Renk72Lk.DataAccess.Repositories;

public interface IAttachmentsPointRepository
{
    Task<int> CreateAsync(AttachmentsPointEntity entity);
    void Delete(int id);
    AttachmentsPointEntity[] GetAll();
    AttachmentsPointEntity GetById(int id);
    void Update(AttachmentsPointEntity entity);
}