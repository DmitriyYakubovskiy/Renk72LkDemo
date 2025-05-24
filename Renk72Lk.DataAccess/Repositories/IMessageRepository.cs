using Renk72Lk.DataAccess.Entities;

namespace Renk72Lk.DataAccess.Repositories;

public interface IMessageRepository
{
    MessageEntity[] GetAll(int bidId);
    MessageEntity[] GetAll();
    MessageEntity GetById(int id);
    Task CreateAsync(MessageEntity ticket);
    void Update(MessageEntity entity);
    void Delete(int id);
}