using Renk72Lk.Models.DataBase;

namespace Renk72Lk.Services.DataBase;

public interface IBidAttachmentsService
{
    BidAttachmentsModel[] GetAll();
    BidAttachmentsModel GetById(int id);
    BidAttachmentsModel GetOne(int userId, int bidI);
    Task CreateAsync(BidAttachmentsModel model);
    Task UpdateAsync(BidAttachmentsModel model);
    void Delete(int id);
}
