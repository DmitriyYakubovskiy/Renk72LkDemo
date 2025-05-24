using Renk72Lk.DataAccess.Entities;

namespace Renk72Lk.DataAccess.Repositories;

public interface IBidAttachmentsRepository
{
    BidAttachmentsEntity[] GetAll();
    BidAttachmentsEntity GetById(int id);
    BidAttachmentsEntity GetOne(int userId, int bidId);
    Task CreateAsync(BidAttachmentsEntity entity);
    void Update(BidAttachmentsEntity entity);
    void Delete(int id);
}
