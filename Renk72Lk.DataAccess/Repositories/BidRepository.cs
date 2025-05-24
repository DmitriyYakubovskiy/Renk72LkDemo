using Microsoft.EntityFrameworkCore;
using Renk72Lk.DataAccess.Contexts;
using Renk72Lk.DataAccess.Entities;

namespace Renk72Lk.DataAccess.Repositories;

public class BidRepository : IBidRepository
{
    private readonly ApplicationDbContext dbContext;

    public BidRepository(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task CreateAsync(BidEntity entity)
    {
        entity.CreatedAt = DateTime.Now;
        entity.UpdatedAt = DateTime.Now;
        await dbContext.Bids.AddAsync(entity);
        dbContext.SaveChanges();
    }

    public void Delete(int id)
    {
        var entity = GetById(id);
        if (entity == null) return;
        dbContext.Bids.Remove(entity);
        dbContext.SaveChanges();
    }

    public BidEntity[] GetAll(bool includeUser = false, bool includeBid1 = false, bool includeBid2 = false, 
        bool includeBid3 = false, bool includeBid4 = false, bool includeBid5 = false, bool includeMessages = false,
        int? status = 0, int? isArchive = null, int? userId = null, string? service = null, string? role = null,
        List<int?> ticketStatuses = null!, string? department = null, string? surname = null, DateTime? date = null, int? take = 100, int? skip = 0)
    {
        var query = dbContext.Bids.AsQueryable();

        if(status != null)
        {
            query = query.Where(x => x.Status == status);
        }
        if(!string.IsNullOrEmpty(department))
        {
            query = query.Where(x => x.Department == department);
        }
        if (!string.IsNullOrEmpty(service))
        {
            query = query.Where(x => x.Service == service);
        }
        if (!string.IsNullOrEmpty(role))
        {
            //query = query.Where(x => x.User.In==role);
        }
        if (ticketStatuses != null)
        {
            if (ticketStatuses.Count != 0)
            {
                query = query.Where(x => ticketStatuses.Contains(x.TicketStatus));
            }
        }
        if (!string.IsNullOrEmpty(surname))
        {
            query = query.Where(x => x.User.Surname!.ToLower().Contains(surname.ToLower()));
        }
        if(userId != null)
        {
            query = query.Where(x => x.UserId == userId);
        }
        if (date != null)
        {
            query = query.Where(x => x.UpdatedAt!.Value.Date == date.Value.Date);
        }
        if (isArchive != null)
        {
            query = query.Where(x => x.IsArchive == isArchive);
        }
        if (includeUser) query = query.Include(x => x.User).ThenInclude(x => x.ActualAddress).Include(x => x.User).ThenInclude(x => x.RegistrationAddress)
                .Include(x => x.User).ThenInclude(x => x.UserDataAgreementFile);
        if (includeBid1) query = query.Include(x => x.Step1).ThenInclude(x => x.ActualAddress).Include(x => x.Step1).ThenInclude(x => x.RegistrationAddress);
        if (includeBid2) query = query.Include(x => x.Step2);
        if (includeBid3) query = query.Include(x => x.Step3);
        if (includeBid4) query = query.Include(x => x.Step4).ThenInclude(x=>x.Points).Include(x=>x.Step4).ThenInclude(x=>x.Stages);
        if (includeBid5) query = query.Include(x => x.Step5).Include(x => x.Step5).ThenInclude(s => s.PassportFile).Include(x => x.Step5).ThenInclude(s => s.PowerDevicesPlanFile)
               .Include(x => x.Step5).ThenInclude(s => s.SnilsFile).Include(x => x.Step5).ThenInclude(s => s.BenefitFile).Include(x => x.Step5).ThenInclude(s => s.OtherFile);
        if (includeMessages) query = query.Include(x => x.Messages).ThenInclude(x=>x.User).Include(x => x.Messages).ThenInclude(x => x.File);
        query = query.OrderByDescending(x => x.CreatedAt);
        if (skip != null)
        {
            query = query.Skip(skip.Value);
        }
        if (take != null)
        {
            query = query.Take(take.Value);
        }

        return query.ToArray();
    }

    public BidEntity GetOne(int userId, int status, DateTime? date = null,
        bool includeUser = true, bool includeBid1 = true, bool includeBid2 = true,
        bool includeBid3 = true, bool includeBid4 = true, bool includeBid5 = true,
        bool includeMessages = false)
    {
        var query = dbContext.Bids.AsQueryable();

        if (date == null)
        {
            query = query.Where(x => x.UserId == userId).Where(x => x.Status == status).OrderByDescending(x => x.CreatedAt);
        }
        else
        {
            query = query.Where(x => x.UserId == userId).Where(x => x.Status == status).Where(x => x.CreatedAt >= date).OrderByDescending(x => x.CreatedAt);
        }
        if (includeUser) query = query.Include(x => x.User).ThenInclude(x => x.ActualAddress).Include(x => x.User).ThenInclude(x => x.RegistrationAddress)
                .Include(x=>x.User).ThenInclude(x=>x.UserDataAgreementFile);
        if (includeBid1) query = query.Include(x => x.Step1).ThenInclude(x => x.ActualAddress).Include(x => x.Step1).ThenInclude(x => x.RegistrationAddress);
        if (includeBid2) query = query.Include(x => x.Step2);
        if (includeBid3) query = query.Include(x => x.Step3);
        if (includeBid4) query = query.Include(x => x.Step4).ThenInclude(x => x.Points).Include(x => x.Step4).ThenInclude(x => x.Stages);
        if (includeBid5) query = query.Include(x => x.Step5).Include(x => x.Step5).ThenInclude(s => s.PassportFile).Include(x => x.Step5).ThenInclude(s => s.PowerDevicesPlanFile)
               .Include(x => x.Step5).ThenInclude(s => s.SnilsFile).Include(x => x.Step5).ThenInclude(s => s.BenefitFile).Include(x => x.Step5).ThenInclude(s => s.OtherFile);
        if (includeMessages) query = query.Include(x => x.Messages).ThenInclude(x => x.User).Include(x => x.Messages).ThenInclude(x => x.File);

        return query.Include(x => x.DocumentFile).FirstOrDefault()!;
    }

    public BidEntity GetById(int id, bool includeUser = false, bool includeBid1 = false, bool includeBid2 = false,
        bool includeBid3 = false, bool includeBid4 = false, bool includeBid5 = false, bool includeMessages = false)
    {
        var query = dbContext.Bids.AsQueryable();

        if (includeUser) query = query.Include(x => x.User).ThenInclude(x => x.ActualAddress).Include(x => x.User).ThenInclude(x => x.RegistrationAddress)
                .Include(x => x.User).ThenInclude(x => x.UserDataAgreementFile);
        if (includeBid1) query = query.Include(x => x.Step1).ThenInclude(x => x.ActualAddress).Include(x => x.Step1).ThenInclude(x => x.RegistrationAddress);
        if (includeBid2) query = query.Include(x => x.Step2);
        if (includeBid3) query = query.Include(x => x.Step3);
        if (includeBid4) query = query.Include(x => x.Step4).ThenInclude(x => x.Points).Include(x => x.Step4).ThenInclude(x => x.Stages);
        if (includeBid5) query = query.Include(x => x.Step5).Include(x => x.Step5).ThenInclude(s => s.PassportFile).Include(x => x.Step5).ThenInclude(s => s.PowerDevicesPlanFile)
               .Include(x => x.Step5).ThenInclude(s => s.SnilsFile).Include(x => x.Step5).ThenInclude(s => s.BenefitFile).Include(x => x.Step5).ThenInclude(s => s.OtherFile);
        if (includeMessages) query = query.Include(x => x.Messages).ThenInclude(x => x.User).Include(x => x.Messages).ThenInclude(x => x.File);

        return query.Include(x => x.DocumentFile).FirstOrDefault(x => x.Id == id)!;
    }

    public void Update(BidEntity entity)
    {
        entity.UpdatedAt = DateTime.Now;
        dbContext.Update(entity);
        dbContext.SaveChanges();
    }
}
