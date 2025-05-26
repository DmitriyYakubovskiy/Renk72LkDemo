using Renk72Lk.Models.DataBase;
using Renk72Lk.ViewModels;

namespace Renk72Lk.Services.DataBase;

public interface IFileService
{
    AttachmentFileModel[] GetAll();
    AttachmentFileModel GetById(int id);
    Task<int> CreateMessageFileAsync(IFormFile formFile);
    Task<int> CreateBidDocumentFileAsync(CreateBidViewModel viewBid);
    Task<int> CreateBidAttachmentsFileAsync(IFormFile[] formFiles);
    Task<int> CreateUserDataAgreementFileAsync(IFormFile formFile);
    Task<int> CreateAsync(AttachmentFileModel model);
    void Update(AttachmentFileModel model);
    void Delete(int id);
}
