using Renk72Lk.Models.DataBase;
using Renk72Lk.ViewModels;

namespace Renk72Lk.Services.DataBase;

public interface IFileService
{
    AttachmentFileModel[] GetAll();
    AttachmentFileModel GetById(int id);
    Task<AttachmentFileModel> CreateMessageFileAsync(IFormFile formFile);
    Task<AttachmentFileModel> CreateBidDocumentFileAsync(CreateBidViewModel viewBid);
    Task<AttachmentFileModel> CreateBidAttachmentsFileAsync(IFormFile[] formFiles);
    Task<AttachmentFileModel> CreateUserDataAgreementFileAsync(IFormFile formFile);
    Task<AttachmentFileModel> CreateAsync(AttachmentFileModel model);
    void Update(AttachmentFileModel model);
    void Delete(int id);
}
