using Renk72Lk.Models.DataBase;

namespace Renk72Lk.Services.DataBase;

public interface IBidRepresentativeInfoService
{
    BidRepresentativeInfoModel[] GetAll();
    BidRepresentativeInfoModel GetById(int id);
    BidRepresentativeInfoModel GetOne(int userId, int bidI);
    BidRepresentativeInfoModel GetRepresentative(int userId);
    Task CreateAsync(BidRepresentativeInfoModel model);
    Task UpdateAsync(BidRepresentativeInfoModel model);
    void Delete(int id);
}