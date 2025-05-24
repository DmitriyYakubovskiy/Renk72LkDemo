using Microsoft.EntityFrameworkCore;
using Renk72Lk.DataAccess.Contexts;
using Renk72Lk.DataAccess.Entities;

namespace Renk72Lk.DataAccess.Repositories;

public class MessageRepository : IMessageRepository
{
    private readonly ApplicationDbContext dbContext;

    public MessageRepository(ApplicationDbContext context)
    {
        dbContext = context;
    }

    public async Task CreateAsync(MessageEntity entity)
    {
        entity.CreatedAt = DateTime.Now;
        entity.UpdatedAt = DateTime.Now;
        await dbContext.Messages.AddAsync(entity);
        dbContext.SaveChanges();
    }

    public MessageEntity[] GetAll()
    {
        return dbContext.Messages.Include(x => x.User).Include(x=>x.File).ToArray();
    }
 
    public MessageEntity GetById(int id)
    {
        return dbContext.Messages.Include(x=>x.User).Include(x=>x.File).FirstOrDefault(t => t.Id == id)!;
    }

    public void Update(MessageEntity entity)
    {
        var ticket = GetById(entity.Id);
        if (ticket == null) return;

        ticket.UpdatedAt = DateTime.Now;
        dbContext.Messages.Update(ticket);
        dbContext.SaveChanges();

    }

    public void Delete(int id)
    {
        var ticket = GetById(id);
        if (ticket == null) return ;

        dbContext.Messages.Remove(ticket);
        dbContext.SaveChanges();
    }

    public MessageEntity[] GetAll(int bidId)
    {
        return dbContext.Messages.Where(x => x.BidId == bidId).Include(x => x.User).Include(x => x.File).ToArray();
    }
}
