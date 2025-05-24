using AutoMapper;
using Renk72Lk.DataAccess.Entities;
using Renk72Lk.DataAccess.Repositories;
using Renk72Lk.Models.DataBase;

namespace Renk72Lk.Services.DataBase;

public class BidConnectionObjectInfoService : IBidConnectionObjectInfoService
{
    private readonly IBidConnectionObjectInfoRepository repository;
    private readonly IMapper mapper;

    public BidConnectionObjectInfoService(IBidConnectionObjectInfoRepository repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task CreateAsync(BidConnectionObjectInfoModel model)
    {
        var entity = mapper.Map<BidConnectionObjectInfoEntity>(model);
        await repository.CreateAsync(entity);
    }

    public void Delete(int id)
    {
        repository.Delete(id);
    }

    public BidConnectionObjectInfoModel[] GetAll()
    {
        var entities = repository.GetAll();
        return mapper.Map<BidConnectionObjectInfoModel[]>(entities);
    }

    public BidConnectionObjectInfoModel GetOne(int userId, int bidId)
    {
        var entity = repository.GetOne(userId, bidId);
        return mapper.Map<BidConnectionObjectInfoModel>(entity);
    }

    public BidConnectionObjectInfoModel GetById(int id)
    {
        var entity = repository.GetById(id);
        return mapper.Map<BidConnectionObjectInfoModel>(entity);
    }

    public void Update(BidConnectionObjectInfoModel model)
    {
        var entity = repository.GetById(model.Id);
        if (entity == null) return;

        entity.UserId = model.UserId;
        entity.BidId = model.BidId;
        entity.Region = model.Region!;
        entity.District = model.District!;
        entity.AddressOfObject = model.AddressOfObject!;
        entity.CadastralNumber = model.CadastralNumber!;
        entity.ReasonForBid = model.ReasonForBid!;
        entity.GuarantySupplier = model.GuarantySupplier!;
        entity.TypeOfContract = model.TypeOfContract!;
        entity.VoltageClass = model.VoltageClass!;
        entity.ConnectionType = model.ConnectionType!;
        entity.PowerDevice = model.PowerDevice!;
        entity.ObjectName = model.ObjectName!;

        repository.Update(entity);
    }
}
