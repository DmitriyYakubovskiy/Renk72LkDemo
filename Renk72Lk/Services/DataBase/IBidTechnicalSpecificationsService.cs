using Renk72Lk.Models.DataBase;

namespace Renk72Lk.Services.DataBase;

public interface IBidTechnicalSpecificationsService
{
    BidTechnicalSpecificationsModel[] GetAll();
    BidTechnicalSpecificationsModel GetById(int id);
    BidTechnicalSpecificationsModel GetOne(int userId, int bidI);
    Task CreateAsync(BidTechnicalSpecificationsModel model);
    void Update(BidTechnicalSpecificationsModel model);
    void Delete(int id);
}