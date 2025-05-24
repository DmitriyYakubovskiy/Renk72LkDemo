using AutoMapper;
using Renk72Lk.DataAccess.Entities;
using Renk72Lk.DataAccess.Repositories;
using Renk72Lk.Models.DataBase;

namespace Renk72Lk.Services.DataBase;

public class BidService : IBidService
{
    private readonly IBidRepository repository;
    private readonly IMapper mapper;

    public BidService(IBidRepository repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task CreateAsync(BidModel model)
    {
        var entity = mapper.Map<BidEntity>(model);
        await repository.CreateAsync(entity);
    }

    public void Delete(int id)
    {
        repository.Delete(id);
    }

    public BidModel[] GetAll(bool includeUser = true, bool includeBid1 = false, bool includeBid2 = false,
        bool includeBid3 = false, bool includeBid4 = false, bool includeBid5 = false, bool includeTickets = false,
        int? status = 0, int? isArchive = null, int? userId = null, string? service = null, string? role = null, 
        List<int?> ticketStatuses = null!, string? department = null, string? surname = null, DateTime? date = null,int? take = 100, int? skip = 0)
    {
        var entities = repository.GetAll(includeUser:includeUser, includeBid1:includeBid1, includeBid2:includeBid2,
            includeBid3:includeBid3,includeBid4:includeBid4,includeBid5:includeBid5, includeTickets: includeTickets,
            status:status,isArchive:isArchive, userId:userId, service:service, role:role, ticketStatuses:ticketStatuses, surname:surname, date: date, take: take, skip: skip);
        return mapper.Map<BidModel[]>(entities);
    }

    public BidModel GetById(int id, bool includeUser = false, bool includeBid1 = false, bool includeBid2 = false,
        bool includeBid3 = false, bool includeBid4 = false, bool includeBid5 = false, bool includeTickets = false)
    {
        var entity = repository.GetById(id, includeUser: includeUser, includeBid1: includeBid1,
             includeBid2: includeBid2, includeBid3: includeBid3, includeBid4: includeBid4, includeBid5: includeBid5, includeTickets: includeTickets);
        return mapper.Map<BidModel>(entity);
    }

    public BidModel GetOne(int userId, int status, DateTime? date = null,
        bool includeUser = true, bool includeBid1 = true, bool includeBid2 = true,
        bool includeBid3 = true, bool includeBid4 = true, bool includeBid5 = true,
        bool includeTickets = false)
    {
        var entity = repository.GetOne(userId, status: status, date: date, includeUser: includeUser, includeBid1:includeBid1,
             includeBid2: includeBid2, includeBid3: includeBid3, includeBid4: includeBid4, includeBid5: includeBid5, includeTickets: includeTickets);      
        return mapper.Map<BidModel>(entity);
    }

    public void Update(BidModel model)
    {
        var entity = repository.GetById(model.Id);
        if (entity == null) return;

        entity.UserId = model.UserId;
        entity.UserRole = model.UserRole;
        entity.Name = model?.Name;
        entity.Department = model?.Department;
        entity.Service = model?.Service;
        entity.DocumentFileId = model.DocumentFileId;
        entity.Status =  model?.Status;
        entity.TicketStatus = model?.TicketStatus;
        entity.IsArchive = model?.IsArchive;

        repository.Update(entity);
    }
}
