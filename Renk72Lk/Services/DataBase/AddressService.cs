using AutoMapper;
using Renk72Lk.DataAccess.Entities;
using Renk72Lk.DataAccess.Repositories;
using Renk72Lk.Models.DataBase;

namespace Renk72Lk.Services.DataBase;

public class AddressService : IAddressService
{
    private readonly IAddressRepository repository;
    private readonly IMapper mapper;

    public AddressService(IAddressRepository repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<int> CreateAsync(AddressModel model)
    {
        var entity = mapper.Map<AddressEntity>(model);
        return await repository.CreateAsync(entity);
    }

    public void Delete(int id)
    {
        repository.Delete(id);
    }

    public AddressModel[] GetAll()
    {
        var entities = repository.GetAll();
        return mapper.Map<AddressModel[]>(entities);
    }

    public AddressModel GetById(int id)
    {
        var entity = repository.GetById(id);
        return mapper.Map<AddressModel>(entity);
    }

    public void Update(AddressModel model)
    {
        var entity = repository.GetById(model.Id);
        if (entity == null) return;

        entity.Street = model.Street;
        entity.Office = model.Office;
        entity.House = model.House;
        entity.Build = model.Build;
        entity.Index = model.Index;
        entity.Region = model.Region;

        repository.Update(entity);
    }
}
