using Renk72Lk.DataAccess.Entities;

namespace Renk72Lk.DataAccess.Repositories;

public interface IBidRepository
{
    public BidEntity[] GetAll(bool includeUser = true, bool includeBid1 = false, bool includeBid2 = false,
        bool includeBid3 = false, bool includeBid4 = false, bool includeBid5 = false, bool includeTickets =false,
        int? status = 0, int? isArchive = null, int? userId = null, string? service = null,
        string? role = null, List<int?> ticketStatuses = null!, string? department = null, string? surname = null, DateTime? date = null, int? take = 100, int? skip = 0);
    BidEntity GetOne(int userId, int status, DateTime? date = null,
        bool includeUser = true, bool includeBid1 = true, bool includeBid2 = true,
        bool includeBid3 = true, bool includeBid4 = true, bool includeBid5 = true,
        bool includeTickets = false);
    BidEntity GetById(int id, bool includeUser = false, bool includeBid1 = false, bool includeBid2 = false,
        bool includeBid3 = false, bool includeBid4 = false, bool includeBid5 = false, bool includeTickets = false);
    Task CreateAsync(BidEntity entity);
    void Update(BidEntity entity);
    void Delete(int id);
}