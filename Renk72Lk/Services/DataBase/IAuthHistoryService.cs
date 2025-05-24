using Renk72Lk.Models.DataBase;

namespace Renk72Lk.Services.DataBase;

public interface IAuthHistoryService
{
    AuthHistoryModel[] GetAll();
    AuthHistoryModel[] GetAll(int userId, int lastCount=-1);
    AuthHistoryModel GetById(int id);
    Task CreateAsync(AuthHistoryModel model);
    void Update(AuthHistoryModel model);
    void Delete(int id);
}
