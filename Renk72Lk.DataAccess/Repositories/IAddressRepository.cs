using Renk72Lk.DataAccess.Entities;

namespace Renk72Lk.DataAccess.Repositories;

public interface IAddressRepository
{
    AddressEntity[] GetAll();
    AddressEntity GetById(int id);
    Task<int> CreateAsync(AddressEntity entity);
    void Update(AddressEntity entity);
    void Delete(int id);
}

