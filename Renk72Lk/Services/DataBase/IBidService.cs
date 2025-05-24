using Renk72Lk.Models.DataBase;

namespace Renk72Lk.Services.DataBase;

public interface IBidService
{
    public BidModel[] GetAll(bool includeUser = true, bool includeBid1 = false, bool includeBid2 = false,
        bool includeBid3 = false, bool includeBid4 = false, bool includeBid5 = false, bool includeTickets = false,
        int? status = 0, int? isArchive = null, int? userId = null, string? service = null,
        string? role = null, List<int?> ticketStatuses = null!, string? department = null, string? surname = null, DateTime? date = null, int? take = 100, int? skip = 0);
    BidModel GetOne(int userId, int status, DateTime? date = null,
        bool includeUser = true, bool includeBid1 = true, bool includeBid2 = true,
        bool includeBid3 = true, bool includeBid4 = true, bool includeBid5 = true
        , bool includeTickets = false);
    BidModel GetById(int id, bool includeUser = false, bool includeBid1 = false, bool includeBid2 = false,
        bool includeBid3 = false, bool includeBid4 = false, bool includeBid5 = false, bool includeTickets = false);
    Task CreateAsync(BidModel model);
    void Update(BidModel model);
    void Delete(int id);
}
