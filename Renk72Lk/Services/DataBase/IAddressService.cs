using Renk72Lk.Models.DataBase;

namespace Renk72Lk.Services.DataBase;

public interface IAddressService
{
    AddressModel[] GetAll();
    AddressModel GetById(int id);
    Task<int> CreateAsync(AddressModel model);
    void Update(AddressModel model);
    void Delete(int id);
}