using AutoMapper;
using Renk72Lk.DataAccess.Entities;
using Renk72Lk.DataAccess.Repositories;
using Renk72Lk.Models.DataBase;

namespace Renk72Lk.Services.DataBase;

public class BidRepresentativeInfoService : IBidRepresentativeInfoService
{
    private readonly IBidRepresentativeInfoRepository repository;
    private readonly IMapper mapper;
    private readonly ILogger<BidRepresentativeInfoService> logger;

    public BidRepresentativeInfoService(IBidRepresentativeInfoRepository repository, IMapper mapper, ILogger<BidRepresentativeInfoService> logger)
    {
        this.repository = repository;
        this.mapper = mapper;
        this.logger = logger;
    }

    public async Task CreateAsync(BidRepresentativeInfoModel model)
    {
        var entity = mapper.Map<BidRepresentativeInfoEntity>(model);
        await repository.CreateAsync(entity);
    }

    public void Delete(int id)
    {
        repository.Delete(id);
    }

    public BidRepresentativeInfoModel[] GetAll()
    {
        var entities = repository.GetAll();
        return mapper.Map<BidRepresentativeInfoModel[]>(entities);
    }

    public BidRepresentativeInfoModel GetOne(int userId, int bidId)
    {
        var entity = repository.GetOne(userId, bidId);
        return mapper.Map<BidRepresentativeInfoModel>(entity);
    }

    public BidRepresentativeInfoModel GetById(int id)
    {
        var entity = repository.GetById(id);
        return mapper.Map<BidRepresentativeInfoModel>(entity);
    }

    public async Task UpdateAsync(BidRepresentativeInfoModel model)
    {
        var entity = repository.GetById(model.Id);
        if (entity == null) return;

        entity.Surname = model.Surname;
        entity.Name = model.Name;
        entity.Patronymic = model.Patronymic;
        entity.Attorney = model.Attorney;
        entity.PassportSeries = model.PassportSeries;
        entity.PassportDate = model.PassportDate;
        entity.PassportNumber = model.PassportNumber;
        entity.PassportIssuedBy = model.PassportIssuedBy;
        entity.PassportType = model.PassportType;
        entity.PassportNumber = model.PassportNumber;
        entity.Email = model.Email;

        repository.Update(entity);
    }

    public BidRepresentativeInfoModel GetRepresentative(int userId)
    {
        var entity = repository.GetRepresentative(userId);
        return mapper.Map<BidRepresentativeInfoModel>(entity);
    }

    private async Task<(string, string)> CreateFileAsync(IFormFile file)
    {
        if (file == null || file.Length == 0) return (null!, null!);

        var fileName = Path.GetFileNameWithoutExtension(file.FileName);
        var fileExtension = Path.GetExtension(file.FileName);

        var newFileName = $"{Guid.NewGuid()}{fileExtension}";

        var filePath = Path.Combine("wwwroot", "uploads", "bid2", newFileName);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return (newFileName, $"/uploads/bid2/{newFileName}");
        }
        catch (Exception ex)
        {
            logger.LogInformation($"Error saving file: {ex.Message}");
            return (null!, null!);
        }
    }
}
