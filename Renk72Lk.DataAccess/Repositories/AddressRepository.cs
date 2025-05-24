using Renk72Lk.DataAccess.Contexts;
using Renk72Lk.DataAccess.Entities;

namespace Renk72Lk.DataAccess.Repositories;

public class AddressRepository : IAddressRepository
{
    private readonly ApplicationDbContext dbContext;

    public AddressRepository(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<int> CreateAsync(AddressEntity entity)
    {
        entity.CreatedAt = DateTime.Now;
        entity.UpdatedAt = DateTime.Now;
        await dbContext.Addresses.AddAsync(entity);
        await dbContext.SaveChangesAsync();
        return entity.Id;
    }

    public void Delete(int id)
    {
        var entity = GetById(id);
        if (entity == null) return;
        dbContext.Addresses.Remove(entity);
        dbContext.SaveChanges();
    }

    public AddressEntity[] GetAll()
    {
        return dbContext.Addresses.ToArray();
    }

    public AddressEntity GetById(int id)
    {
        return dbContext.Addresses.FirstOrDefault(x => x.Id == id)!;
    }

    public void Update(AddressEntity entity)
    {
        entity.UpdatedAt = DateTime.Now;
        dbContext.Addresses.Update(entity);
        dbContext.SaveChanges();
    }
}
