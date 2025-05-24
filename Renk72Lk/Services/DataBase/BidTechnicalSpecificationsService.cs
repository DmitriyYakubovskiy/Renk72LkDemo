using AutoMapper;
using Renk72Lk.DataAccess.Entities;
using Renk72Lk.DataAccess.Repositories;
using Renk72Lk.Models.DataBase;

namespace Renk72Lk.Services.DataBase;

public class BidTechnicalSpecificationsService : IBidTechnicalSpecificationsService
{
    private readonly IBidTechnicalSpecificationsRepository repository;
    private readonly IMapper mapper;

    public BidTechnicalSpecificationsService(IBidTechnicalSpecificationsRepository repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task CreateAsync(BidTechnicalSpecificationsModel model)
    {
        var entity = mapper.Map<BidTechnicalSpecificationsEntity>(model);
        await repository.CreateAsync(entity);
    }

    public void Delete(int id)
    {
        repository.Delete(id);
    }

    public BidTechnicalSpecificationsModel[] GetAll()
    {
        var entities = repository.GetAll();
        return mapper.Map<BidTechnicalSpecificationsModel[]>(entities);
    }

    public BidTechnicalSpecificationsModel GetOne(int userId, int bidId)
    {
        var entity = repository.GetOne(userId, bidId);
        return mapper.Map<BidTechnicalSpecificationsModel>(entity);
    }

    public BidTechnicalSpecificationsModel GetById(int id)
    {
        var entity = repository.GetById(id);
        return mapper.Map<BidTechnicalSpecificationsModel>(entity);
    }

    public void Update(BidTechnicalSpecificationsModel model)
    {
        var entity = repository.GetById(model.Id);
        if (entity == null) return;

        entity.UserId = model.UserId;
        entity.BidId = model.BidId;
        entity.ReliabilityCategory = model.ReliabilityCategory;
        entity.CountOfTransformers = model.CountOfTransformers;
        entity.TransformersPower = model.TransformersPower;
        entity.CountOfGenerators = model.CountOfGenerators;
        entity.GeneratorsPower = model.GeneratorsPower;
        entity.TypeOfLoad = model.TypeOfLoad;
        entity.TechMin = model.TechMin;
        entity.JustificationTechMin = model.JustificationTechMin;

        entity.OldPointPower = model.OldPointPower;
        entity.OldPointVolt = model.OldPointVolt;

        entity.NatureLoad = model.NatureLoad;
        entity.PaymentOrder = model.PaymentOrder;

        repository.Update(entity);
    }
}
