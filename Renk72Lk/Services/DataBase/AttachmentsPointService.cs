using AutoMapper;
using Renk72Lk.DataAccess.Entities;
using Renk72Lk.DataAccess.Repositories;
using Renk72Lk.Models;

namespace Renk72Lk.Services.DataBase;

public class AttachmentsPointService : IAttachmentsPointService
{
    private readonly IAttachmentsPointRepository repository;
    private readonly IMapper mapper;

    public AttachmentsPointService(IAttachmentsPointRepository repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<int> CreateAsync(AttachmentsPointModel model)
    {
        var entity = mapper.Map<AttachmentsPointEntity>(model);
        return await repository.CreateAsync(entity);
    }

    public void Delete(int id)
    {
        repository.Delete(id);
    }

    public AttachmentsPointModel[] GetAll()
    {
        var entities = repository.GetAll();
        return mapper.Map<AttachmentsPointModel[]>(entities);
    }

    public AttachmentsPointModel GetById(int id)
    {
        var entity = repository.GetById(id);
        return mapper.Map<AttachmentsPointModel>(entity);
    }

    public void Update(AttachmentsPointModel model)
    {
        var entity = repository.GetById(model.Id);
        if (entity == null) return;

        entity.Power = model.Power;
        entity.Voltage = model.Voltage;
        entity.TechnicalSpecificationsId = model.TechnicalSpecificationsId;

        repository.Update(entity);
    }
}
