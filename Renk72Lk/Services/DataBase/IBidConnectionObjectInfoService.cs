using Renk72Lk.Models.DataBase;

namespace Renk72Lk.Services.DataBase;

public interface IBidConnectionObjectInfoService
{
    BidConnectionObjectInfoModel[] GetAll();
    BidConnectionObjectInfoModel GetById(int id);
    BidConnectionObjectInfoModel GetOne(int userId, int bidI);
    Task CreateAsync(BidConnectionObjectInfoModel model);
    void Update(BidConnectionObjectInfoModel model);
    void Delete(int id);
}
