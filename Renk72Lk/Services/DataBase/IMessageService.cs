using Renk72Lk.Models.DataBase;

namespace Renk72Lk.Services.DataBase;

public interface IMessageService
{
    MessageModel[] GetAll(int bidId);
    MessageModel[] GetAll();
    MessageModel GetById(int id);
    Task<MessageModel> CreateAsync(MessageModel model);
    void Update(MessageModel model);
    void Delete(int id);
}