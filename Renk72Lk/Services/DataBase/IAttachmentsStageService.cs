using Renk72Lk.Models;

namespace Renk72Lk.Services.DataBase;

public interface IAttachmentsStageService
{
    AttachmentsStageModel[] GetAll();
    AttachmentsStageModel GetById(int id);
    Task<int> CreateAsync(AttachmentsStageModel model);
    void Update(AttachmentsStageModel model);
    void Delete(int id);
}
