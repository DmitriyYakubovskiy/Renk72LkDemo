using Renk72Lk.DataAccess.Entities;

namespace Renk72Lk.DataAccess.Repositories;

public interface IAttachmentsStageRepository
{
    Task<int> CreateAsync(AttachmentsStageEntity entity);
    void Delete(int id);
    AttachmentsStageEntity[] GetAll();
    AttachmentsStageEntity GetById(int id);
    void Update(AttachmentsStageEntity entity);
}