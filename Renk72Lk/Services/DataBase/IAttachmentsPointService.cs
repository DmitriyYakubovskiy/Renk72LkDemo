using Renk72Lk.Models;

namespace Renk72Lk.Services.DataBase;

public interface IAttachmentsPointService
{
    AttachmentsPointModel[] GetAll();
    AttachmentsPointModel GetById(int id);
    Task<int> CreateAsync(AttachmentsPointModel model);
    void Update(AttachmentsPointModel model);
    void Delete(int id);
}
