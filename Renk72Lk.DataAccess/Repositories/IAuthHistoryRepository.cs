using Renk72Lk.DataAccess.Entities;

namespace Renk72Lk.DataAccess.Repositories;

public interface IAuthHistoryRepository
{
    AuthHistoryEntity[] GetAll();
    AuthHistoryEntity[] GetAll(int userId, int lastCount=-1);
    AuthHistoryEntity GetById(int id);
    Task CreateAsync(AuthHistoryEntity entity);
    void Update(AuthHistoryEntity entity);
    void Delete(int id);
}
