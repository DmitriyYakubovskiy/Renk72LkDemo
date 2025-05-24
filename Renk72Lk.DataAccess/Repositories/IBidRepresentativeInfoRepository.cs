using Renk72Lk.DataAccess.Entities;

namespace Renk72Lk.DataAccess.Repositories;

public interface IBidRepresentativeInfoRepository
{
    BidRepresentativeInfoEntity[] GetAll();
    BidRepresentativeInfoEntity GetById(int id);
    BidRepresentativeInfoEntity GetOne(int userId, int bidId);
    BidRepresentativeInfoEntity GetRepresentative(int userdId);
    Task CreateAsync(BidRepresentativeInfoEntity entity);
    void Update(BidRepresentativeInfoEntity entity);
    void Delete(int id);
}
