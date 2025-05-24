using Renk72Lk.DataAccess.Entities;

namespace Renk72Lk.DataAccess.Repositories;

public interface IBidConnectionObjectInfoRepository
{
    BidConnectionObjectInfoEntity[] GetAll();
    BidConnectionObjectInfoEntity GetById(int id);
    BidConnectionObjectInfoEntity GetOne(int userId, int bidId);
    Task CreateAsync(BidConnectionObjectInfoEntity entity);
    void Update(BidConnectionObjectInfoEntity entity);
    void Delete(int id);
}