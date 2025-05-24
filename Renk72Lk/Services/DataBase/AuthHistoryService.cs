using AutoMapper;
using Renk72Lk.DataAccess.Entities;
using Renk72Lk.DataAccess.Repositories;
using Renk72Lk.Models.DataBase;

namespace Renk72Lk.Services.DataBase;

public class AuthHistoryService : IAuthHistoryService
{
    private readonly IAuthHistoryRepository repository;
    private readonly IMapper mapper;

    public AuthHistoryService(IAuthHistoryRepository repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task CreateAsync(AuthHistoryModel model)
    {
        var entity = mapper.Map<AuthHistoryEntity>(model);
        await repository.CreateAsync(entity);
    }

    public void Delete(int id)
    {
        repository.Delete(id);
    }

    public AuthHistoryModel[] GetAll()
    {
        var entities = repository.GetAll();
        return mapper.Map<AuthHistoryModel[]>(entities);
    }

    public AuthHistoryModel[] GetAll(int userId, int lastCount = -1)
    {
        var entities = repository.GetAll(userId, lastCount);
        return mapper.Map<AuthHistoryModel[]>(entities);
    }

    public AuthHistoryModel GetById(int id)
    {
        var entity = repository.GetById(id);
        return mapper.Map<AuthHistoryModel>(entity);
    }

    public void Update(AuthHistoryModel model)
    {
        var entity = mapper.Map<AuthHistoryEntity>(model);
        repository.Update(entity);
    }
}
