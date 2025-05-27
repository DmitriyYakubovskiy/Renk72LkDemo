using AutoMapper;
using Renk72Lk.DataAccess.Entities;
using Renk72Lk.DataAccess.Repositories;
using Renk72Lk.Models.DataBase;

namespace Renk72Lk.Services.DataBase;

public class MessageService : IMessageService
{
    private readonly IMessageRepository repository;
    private readonly IMapper mapper;

    public MessageService(IMessageRepository repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<MessageModel> CreateAsync(MessageModel model)
    {
        var entity = mapper.Map<MessageEntity>(model);
        return mapper.Map<MessageModel>(await repository.CreateAsync(entity));
        
    }

    public void Delete(int id)
    {
        repository.Delete(id);
    }

    public MessageModel[] GetAll()
    {
        var entities = repository.GetAll();
        return mapper.Map<MessageModel[]>(entities);
    }

    public MessageModel[] GetAll(int bidId)
    {
        var entities = repository.GetAll(bidId);
        return mapper.Map<MessageModel[]>(entities);
    }

    public MessageModel GetById(int id)
    {
        var entity = repository.GetById(id);
        return mapper.Map<MessageModel>(entity);
    }

    public void Update(MessageModel model)
    {
        var entity = repository.GetById(model.Id);
        if (entity == null) return;

        entity.UserId = model.UserId;
        entity.BidId = model.BidId;
        entity.Message = model?.Message;
        entity.Status = model?.Status;
        entity.FileId = model.FileId;

        repository.Update(entity);
    }
}