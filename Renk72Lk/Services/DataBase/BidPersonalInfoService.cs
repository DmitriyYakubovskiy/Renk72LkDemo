using AutoMapper;
using Renk72Lk.DataAccess.Entities;
using Renk72Lk.DataAccess.Repositories;
using Renk72Lk.Models.DataBase;

namespace Renk72Lk.Services.DataBase;

public class BidPersonalInfoService : IBidPersonalInfoService
{
    private readonly IBidPersonalInfoRepository bidPersonalInfoRepository;
    private readonly IAddressService addressService;
    private readonly IMapper mapper;

    public BidPersonalInfoService(IBidPersonalInfoRepository bidPersonalInfoRepository, IAddressService addressService, IMapper mapper)
    {
        this.bidPersonalInfoRepository = bidPersonalInfoRepository;
        this.addressService = addressService;
        this.mapper = mapper;
    }

    public BidPersonalInfoModel GetConsumer(int userId)
    {
        var entity = bidPersonalInfoRepository.GetConsumer(userId);
        return mapper.Map<BidPersonalInfoModel>(entity);
    }

    public async Task CreateAsync(BidPersonalInfoModel model)
    {
        var entity = mapper.Map<BidPersonalInfoEntity>(model);

        if (model.ActualAddress != null)
        {
            var id = await addressService.CreateAsync(model.ActualAddress);
            entity.ActualAddressId = id;
        }
        if (model.RegistrationAddress != null)
        {
            var id = await addressService.CreateAsync(model.RegistrationAddress);
            entity.RegistrationAddressId = id;
        }
        await bidPersonalInfoRepository.CreateAsync(entity);
    }

    public void Delete(int id)
    {
        bidPersonalInfoRepository.Delete(id);
    }

    public BidPersonalInfoModel[] GetAll()
    {
        var entities = bidPersonalInfoRepository.GetAll();
        return mapper.Map<BidPersonalInfoModel[]>(entities);
    }

    public BidPersonalInfoModel GetById(int id)
    {
        var entity = bidPersonalInfoRepository.GetById(id);
        return mapper.Map<BidPersonalInfoModel>(entity);
    }

    public BidPersonalInfoModel GetOne(int userId, int bidId)
    {
        var entity = bidPersonalInfoRepository.GetOne(userId, bidId);
        return mapper.Map<BidPersonalInfoModel>(entity);
    }

    public async Task<bool> UpdateAsync(BidPersonalInfoModel model)
    {
        var entity = bidPersonalInfoRepository.GetById(model.Id);
        if (entity == null) return false;

        entity.Name = model.Name;
        entity.Surname = model.Surname;
        entity.Patronymic = model.Patronymic;
        entity.Snils = model.Snils;
        entity.PhoneNumber = model.PhoneNumber;
        entity.Email = model.Email;
        entity.PassportType = model.PassportType;
        entity.PassportSeries = model.PassportSeries;
        entity.PassportNumber = model.PassportNumber;
        entity.PassportDate = model.PassportDate;
        entity.PassportIssuedBy = model.PassportIssuedBy;
        entity.IsAgreePersonData = model.IsAgreePersonData;
        entity.DateOfBirth = model.DateOfBirth;
        entity.PlaceOfBirth = model.PlaceOfBirth;
        entity.PassportIssuedBy = model.PassportIssuedBy;

        if (entity.ActualAddressId == null)
        {
            var id = await addressService.CreateAsync(model.ActualAddress);
            entity.ActualAddressId = id;
        }
        else
        {
            model.ActualAddress.Id = entity.ActualAddressId!.Value;
            addressService.Update(model.ActualAddress);
        }
        if (entity.RegistrationAddressId == null)
        {
            var id = await addressService.CreateAsync(model.RegistrationAddress);
            entity.RegistrationAddressId = id;
        }
        else
        {
            model.RegistrationAddress.Id = entity.RegistrationAddressId!.Value;
            addressService.Update(model.RegistrationAddress);
        }

        bidPersonalInfoRepository.Update(entity);
        return true;
    }
}
