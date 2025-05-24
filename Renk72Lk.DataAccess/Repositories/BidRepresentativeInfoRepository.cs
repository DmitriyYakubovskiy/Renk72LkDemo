using Renk72Lk.DataAccess.Contexts;
using Renk72Lk.DataAccess.Entities;

namespace Renk72Lk.DataAccess.Repositories;

public class BidRepresentativeInfoRepository : IBidRepresentativeInfoRepository
{
    private readonly ApplicationDbContext dbContext;

    public BidRepresentativeInfoRepository(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task CreateAsync(BidRepresentativeInfoEntity entity)
    {
        entity.CreatedAt = DateTime.Now;
        entity.UpdatedAt = DateTime.Now;
        await dbContext.RepresentativeInfos.AddAsync(entity);
        dbContext.SaveChanges();
    }

    public void Delete(int id)
    {
        var entity = GetById(id);
        if (entity == null) return;
        dbContext.RepresentativeInfos.Remove(entity);
        dbContext.SaveChanges();
    }

    public BidRepresentativeInfoEntity[] GetAll()
    {
        return dbContext.RepresentativeInfos.ToArray();
    }

    public BidRepresentativeInfoEntity GetOne(int userId, int bidId)
    {
        return dbContext.RepresentativeInfos.Where(e => e.UserId == userId).Where(x => x.BidId == bidId)
            .OrderByDescending(x => x.UpdatedAt).FirstOrDefault()!;
    }

    public BidRepresentativeInfoEntity GetById(int id)
    {
        return dbContext.RepresentativeInfos.FirstOrDefault(x => x.Id == id)!;
    }

    public void Update(BidRepresentativeInfoEntity entity)
    {
        entity.UpdatedAt = DateTime.Now;
        dbContext.Update(entity);
        dbContext.SaveChanges();
    }

    public BidRepresentativeInfoEntity GetRepresentative(int userId)
    {
        return dbContext.RepresentativeInfos.Where(e => e.UserId == userId)
            .Where(x => x.Name != null).Where(x => x.Surname != null)
            .Where(x => x.Patronymic != null).Where(x => x.Attorney != null)
            .Where(x => x.Email != null)
            .OrderByDescending(x => x.UpdatedAt).FirstOrDefault()!;
    }
}
