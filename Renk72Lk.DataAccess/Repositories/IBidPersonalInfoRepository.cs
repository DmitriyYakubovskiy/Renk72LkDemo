using Microsoft.EntityFrameworkCore;
using Renk72Lk.DataAccess.Entities;

namespace Renk72Lk.DataAccess.Repositories;

public interface IBidPersonalInfoRepository
{
    BidPersonalInfoEntity[] GetAll();
    BidPersonalInfoEntity GetById(int id);
    BidPersonalInfoEntity GetOne(int userId, int bidId);
    BidPersonalInfoEntity GetConsumer(int userId);
    Task CreateAsync(BidPersonalInfoEntity entity);
    void Update(BidPersonalInfoEntity entity);
    void Delete(int id);
}