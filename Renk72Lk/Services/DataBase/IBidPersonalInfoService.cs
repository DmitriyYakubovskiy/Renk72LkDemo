using Renk72Lk.Models.DataBase;

namespace Renk72Lk.Services.DataBase;

public interface IBidPersonalInfoService
{
    BidPersonalInfoModel[] GetAll();
    BidPersonalInfoModel GetById(int id);
    BidPersonalInfoModel GetOne(int userId, int bidId);
    BidPersonalInfoModel GetConsumer(int userId);
    Task CreateAsync(BidPersonalInfoModel model);
    Task<bool> UpdateAsync(BidPersonalInfoModel model);
    void Delete(int id);
}
