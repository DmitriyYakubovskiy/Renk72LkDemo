using AutoMapper;
using Renk72Lk.DataAccess.Entities;
using Renk72Lk.DataAccess.Repositories;
using Renk72Lk.Models;
using Renk72Lk.Models.DataBase;

namespace Renk72Lk.Services.DataBase;

public class BidAttachmentsService : IBidAttachmentsService
{
    private readonly IBidAttachmentsRepository repository;
    private readonly IFileService fileService;
    private readonly IMapper mapper;

    public BidAttachmentsService(IMapper mapper, IBidAttachmentsRepository repository, IFileService fileService)
    {
        this.mapper = mapper;
        this.repository = repository;
        this.fileService = fileService;
    }

    public async Task CreateAsync(BidAttachmentsModel model)
    {
        var entity = mapper.Map<BidAttachmentsEntity>(model);
        await repository.CreateAsync(entity);
    }

    public void Delete(int id)
    {
        repository.Delete(id);
    }

    public BidAttachmentsModel[] GetAll()
    {
        var entities = repository.GetAll();
        return mapper.Map<BidAttachmentsModel[]>(entities);
    }

    public BidAttachmentsModel GetOne(int userId, int bidId)
    {
        var entity = repository.GetOne(userId, bidId);
        return mapper.Map<BidAttachmentsModel>(entity);
    }

    public BidAttachmentsModel GetById(int id)
    {
        var entity = repository.GetById(id);
        return mapper.Map<BidAttachmentsModel>(entity);
    }

    public async Task UpdateAsync(BidAttachmentsModel model)
    {
        var entity = repository.GetById(model.Id);
        if (entity == null) return;

        entity.UserId = model.UserId;
        entity.BidId = model.BidId; 
        entity.IsAgreePreviewPDF = model.IsAgreePreviewPDF;

        if (entity.OtherFileId != model.OtherFileId) entity.OtherFileId = model.OtherFileId;
        if (entity.PassportFileId != model.PassportFileId) entity.PassportFileId = model.PassportFileId;
        if (entity.SnilsFileId != model.SnilsFileId) entity.SnilsFileId = model.SnilsFileId;
        if (entity.PowerDevicesPlanFileId != model.PowerDevicesPlanFileId) entity.PowerDevicesPlanFileId = model.PowerDevicesPlanFileId;
        if (entity.BenefitFileId != model.BenefitFileId) entity.BenefitFileId = model.BenefitFileId;
        if (model?.OtherFiles?.Length != 0 && model.OtherFiles != null)
        {
            int id = (await fileService.CreateBidAttachmentsFileAsync(model.OtherFiles)).Id;
            entity.OtherFileId = id;
        }
        if (model?.PassportFiles?.Length != 0 && model.PassportFiles != null)
        {
            int id = (await fileService.CreateBidAttachmentsFileAsync(model.PassportFiles)).Id;
            entity.PassportFileId = id;
        }
        if (model?.SnilsFiles?.Length != 0 && model.SnilsFiles != null)
        {
            int id = (await fileService.CreateBidAttachmentsFileAsync(model.SnilsFiles)).Id;
            entity.SnilsFileId = id;
        }
        if (model?.PlanFiles?.Length != 0 && model.PlanFiles != null)
        {
            int id = (await fileService.CreateBidAttachmentsFileAsync(model.PlanFiles)).Id;
            entity.PowerDevicesPlanFileId = id;
        }
        if (model?.BenefitFiles?.Length != 0 && model.BenefitFiles != null)
        {
            int id = (await fileService.CreateBidAttachmentsFileAsync(model.BenefitFiles)).Id;
            entity.BenefitFileId = id;
        }

        repository.Update(entity);
    }
}
