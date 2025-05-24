using Renk72Lk.DataAccess.Entities;

namespace Renk72Lk.DataAccess.Repositories;

public interface IBidTechnicalSpecificationsRepository
{
    BidTechnicalSpecificationsEntity[] GetAll();
    BidTechnicalSpecificationsEntity GetById(int id);
    BidTechnicalSpecificationsEntity GetOne(int userId, int bidId);
    Task CreateAsync(BidTechnicalSpecificationsEntity entity);
    void Update(BidTechnicalSpecificationsEntity entity);
    void Delete(int id);
}