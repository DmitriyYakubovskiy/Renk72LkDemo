using AutoMapper;
using Renk72Lk.DataAccess.Entities;
using Renk72Lk.DataAccess.Repositories;
using Renk72Lk.Models;

namespace Renk72Lk.Services.DataBase;

public class AttachmentsStageService : IAttachmentsStageService
{
    private readonly IAttachmentsStageRepository repository;
    private readonly IMapper mapper;

    public AttachmentsStageService(IAttachmentsStageRepository repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<int> CreateAsync(AttachmentsStageModel model)
    {
        var entity = mapper.Map<AttachmentsStageEntity>(model);
        return await repository.CreateAsync(entity);
    }

    public void Delete(int id)
    {
        repository.Delete(id);
    }

    public AttachmentsStageModel[] GetAll()
    {
        var entities = repository.GetAll();
        return mapper.Map<AttachmentsStageModel[]>(entities);
    }

    public AttachmentsStageModel GetById(int id)
    {
        var entity = repository.GetById(id);
        return mapper.Map<AttachmentsStageModel>(entity);
    }

    public void Update(AttachmentsStageModel model)
    {
        var entity = repository.GetById(model.Id);
        if (entity == null) return;

        entity.Power = model.Power;
        entity.DesignPeriod = model.DesignPeriod;
        entity.CommissioningPeriod = model.CommissioningPeriod;
        entity.TechnicalSpecificationsId = model.TechnicalSpecificationsId;

        repository.Update(entity);
    }
}
