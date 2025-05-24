using AutoMapper;
using Microsoft.Extensions.Options;
using Renk72Lk.DataAccess.Entities;
using Renk72Lk.DataAccess.Repositories;
using Renk72Lk.Models.DataBase;
using Renk72Lk.Settings;
using Renk72Lk.ViewModels;
using System.IO.Compression;

namespace Renk72Lk.Services.DataBase;

public class AttachmentFileService : IAttachmentFileService
{
    private readonly IAttachmentFileRepository repository;
    private readonly IMapper mapper;
    private readonly HttpClient httpClient;
    private readonly PdfGeneratorApiSettings apiSettings;

    public AttachmentFileService(IAttachmentFileRepository repository, IMapper mapper, HttpClient httpClient,
        IOptions<PdfGeneratorApiSettings> apiSettings)
    {
        this.repository = repository;
        this.mapper = mapper;
        this.httpClient = httpClient;
        this.apiSettings = apiSettings.Value;
    }

    public async Task<int> CreateMessageFileAsync(IFormFile formFile)
    {
        var uploadDir = Path.Combine("wwwroot", "uploads", "documents");
        try
        {
            if (formFile == null || formFile.Length == 0) return -1;
            if (!Directory.Exists(uploadDir)) Directory.CreateDirectory(uploadDir);

            var fileExtension = Path.GetExtension(formFile.FileName);
            var baseFileName = Path.GetFileNameWithoutExtension(formFile.FileName);
            int counter = 0;
            string fileName = $"{baseFileName}{fileExtension}";
            string filePath = Path.Combine(uploadDir, fileName);

            while (File.Exists(filePath))
            {
                counter++;
                fileName = $"{(counter > 0 ? $"{counter}_" : "")}{baseFileName}{fileExtension}";
                filePath = Path.Combine("wwwroot/uploads/documents/", fileName);
            }

            using (var stream = new FileStream(filePath, FileMode.Create)) await formFile.CopyToAsync(stream);

            var model = new AttachmentFileModel()
            {
                FileName = fileName,
                FilePath = $"/uploads/documents/{fileName}",
            };

            return await CreateAsync(model);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return -1;
    }

    public async Task<int> CreateBidDocumentFileAsync(CreateBidViewModel viewBid)
    {
        var uploadDir = Path.Combine("wwwroot", "uploads", "documents");
        try
        {
            if (!Directory.Exists(uploadDir)) Directory.CreateDirectory(uploadDir);

            var response = await httpClient.PostAsJsonAsync($"http://{apiSettings.Host}:{apiSettings.Port}/api/generate-pdf", viewBid.Bid);

            if (response.IsSuccessStatusCode)
            {
                var pdfBytes = await response.Content.ReadAsByteArrayAsync();
                var fileName = $"{Guid.NewGuid()}.pdf";
                var filePath = Path.Combine(uploadDir, fileName);

                await File.WriteAllBytesAsync(filePath, pdfBytes);

                var model = new AttachmentFileModel()
                {
                    FileName = fileName,
                    FilePath = $"/uploads/documents/{fileName}",
                };

                return await CreateAsync(model);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return -1;
    }

    public async Task<int> CreateBidAttachmentsFileAsync(IFormFile[] formFiles)
    {
        var uploadDir = Path.Combine("wwwroot", "uploads", "bid5");
        try
        {
            if (formFiles == null || formFiles.Length == 0) return -1;
            if (!Directory.Exists(uploadDir)) Directory.CreateDirectory(uploadDir);

            var zipFileName = $"{Guid.NewGuid()}.zip";
            var zipPath = Path.Combine(uploadDir, zipFileName);

            using (var zipStream = new FileStream(zipPath, FileMode.Create))
            using (var archive = new ZipArchive(zipStream, ZipArchiveMode.Create))
            {
                foreach (var formFile in formFiles)
                {
                    if (formFile.Length == 0) continue;

                    var entryName = $"{Path.GetFileNameWithoutExtension(formFile.FileName)}_{Guid.NewGuid()}{Path.GetExtension(formFile.FileName)}";
                    var entry = archive.CreateEntry(entryName);

                    using (var entryStream = entry.Open())
                    using (var fileStream = formFile.OpenReadStream())
                    {
                        await fileStream.CopyToAsync(entryStream);
                    }
                }
            }

            var model = new AttachmentFileModel
            {
                FileName = zipFileName,
                FilePath = $"/uploads/bid5/{zipFileName}",
            };

            return await CreateAsync(model);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving formFile: {ex.Message}");
            return -1;
        }
    }

    public async Task<int> CreateUserDataAgreementFileAsync(IFormFile formFile)
    {
        var uploadDir = Path.Combine("wwwroot", "uploads", "sopd");
        try
        {
            if (formFile == null || formFile.Length == 0) return -1;
            if (!Directory.Exists(uploadDir)) Directory.CreateDirectory(uploadDir);

            var fileExtension = Path.GetExtension(formFile.FileName);
            var fileName = string.Format(@"{0}", Guid.NewGuid()) + $"{fileExtension}";
            var filePath = Path.Combine(uploadDir, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create)) await formFile.CopyToAsync(stream);

            var model = new AttachmentFileModel()
            {
                FileName = fileName,
                FilePath = $"/uploads/sopd/{fileName}",
            };

            return await CreateAsync(model);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return -1;
    }

    public async Task<int> CreateAsync(AttachmentFileModel model)
    {
        var entity = mapper.Map<AttachmentFileEntity>(model);
        return await repository.CreateAsync(entity);
    }

    public void Delete(int id)
    {
        repository.Delete(id);
    }

    public AttachmentFileModel[] GetAll()
    {
        var entities = repository.GetAll();
        return mapper.Map<AttachmentFileModel[]>(entities);
    }

    public AttachmentFileModel GetById(int id)
    {
        var entity = repository.GetById(id);
        return mapper.Map<AttachmentFileModel>(entity);
    }

    public void Update(AttachmentFileModel model)
    {
        var entity = repository.GetById(model.Id);
        if (entity == null) return;

        entity.FilePath = model.FilePath;
        entity.FileName = model.FileName;

        repository.Update(entity);
    }
}
