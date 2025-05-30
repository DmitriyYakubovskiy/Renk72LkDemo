using AutoMapper;
using Microsoft.Extensions.Options;
using Minio;
using Minio.ApiEndpoints;
using Minio.DataModel.Args;
using Renk72Lk.DataAccess.Entities;
using Renk72Lk.DataAccess.Repositories;
using Renk72Lk.Models.DataBase;
using Renk72Lk.Settings;
using Renk72Lk.ViewModels;
using System.IO.Compression;

namespace Renk72Lk.Services.DataBase;

public class FileService : IFileService
{
    private readonly IAttachmentFileRepository repository;
    private readonly IMapper mapper;
    private readonly HttpClient httpClient;
    private readonly ReportingSettings apiSettings;
    private readonly IMinioClient minioClient;
    private readonly string bucketName;

    public FileService(IAttachmentFileRepository repository, IMapper mapper, HttpClient httpClient,
        IOptions<ReportingSettings> apiSettings, IMinioClient minioClient, IOptions<MinioSettings> minioSettings)
    {
        this.repository = repository;
        this.mapper = mapper;
        this.httpClient = httpClient;
        this.apiSettings = apiSettings.Value;
        this.minioClient = minioClient;
        bucketName = minioSettings.Value.BucketName;
    }

    public async Task<AttachmentFileModel> CreateMessageFileAsync(IFormFile formFile)
    {
        if (formFile == null || formFile.Length == 0) return null!;
        try
        {
            string fileExtension = Path.GetExtension(formFile.FileName);
            string baseFileName = Path.GetFileNameWithoutExtension(formFile.FileName);
            baseFileName = $"{baseFileName}_{GetShortGuid()}";
            string fileName = $"{baseFileName}{fileExtension}";
            string objectPath = Path.Combine("documents/", fileName);
            
            while (repository.FileExists(Path.Combine("/uploads/", objectPath)))
            {
                baseFileName += $"_{GetShortGuid()}";
                fileName = $"{baseFileName}{fileExtension}";
                objectPath = Path.Combine("documents/", fileName);
            }

            await CreateFileInS3(formFile, objectPath);

            var model = new AttachmentFileModel()
            {
                FileName = fileName,
                FilePath = Path.Combine("/uploads/", objectPath),
            };

            return await CreateAsync(model);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return null;
    }

    public async Task<AttachmentFileModel> CreateBidDocumentFileAsync(CreateBidViewModel viewBid)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync($"http://{apiSettings.Host}:{apiSettings.Port}/api/generate-pdf", viewBid.Bid);

            if (response.IsSuccessStatusCode)
            {
                var fileBytes = await response.Content.ReadAsByteArrayAsync();
                string fileName = $"{Guid.NewGuid()}.pdf";
                string objectPath = Path.Combine("documents/", fileName);
                using var memoryStream = new MemoryStream(fileBytes);

                await CreateFileInS3(memoryStream, objectPath, "application/pdf");

                var model = new AttachmentFileModel()
                {
                    FileName = fileName,
                    FilePath = Path.Combine("/uploads/", objectPath),
                };

                return await CreateAsync(model);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return null!;
    }

    public async Task<AttachmentFileModel> CreateBidAttachmentsFileAsync(IFormFile[] formFiles)
    {
        if (formFiles == null || formFiles.Length == 0) return null!;
        try
        {
            var zipFileName = $"{Guid.NewGuid()}.zip";
            var objectPath = Path.Combine("bid5/", zipFileName);

            using var memoryStream = new MemoryStream();
            using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, leaveOpen: true))
            {
                foreach (var formFile in formFiles)
                {
                    if (formFile.Length == 0) continue;

                    var entryName = $"{Path.GetFileNameWithoutExtension(formFile.FileName)}_{GetShortGuid()}{Path.GetExtension(formFile.FileName)}";
                    var entry = archive.CreateEntry(entryName);

                    using var entryStream = entry.Open();
                    using var fileStream = formFile.OpenReadStream();
                    await fileStream.CopyToAsync(entryStream);
                }
            }

            await CreateFileInS3(memoryStream, objectPath, "application/zip");

            var model = new AttachmentFileModel
            {
                FileName = zipFileName,
                FilePath = Path.Combine("/uploads/", objectPath),
            };

            return await CreateAsync(model);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving formFile: {ex.Message}");
            return null!;
        }
    }

    public async Task<AttachmentFileModel> CreateUserDataAgreementFileAsync(IFormFile formFile)
    {
        if (formFile == null || formFile.Length == 0) return null!;
        try
        {
            string fileExtension = Path.GetExtension(formFile.FileName);
            string fileName = $"{Guid.NewGuid()}{fileExtension}";
            string objectPath = Path.Combine("sopd/", fileName);

            await CreateFileInS3(formFile, objectPath);

            var model = new AttachmentFileModel()
            {
                FileName = fileName,
                FilePath = Path.Combine("/uploads/", objectPath),
            };

            return await CreateAsync(model);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return null!;
    }

    public async Task<AttachmentFileModel> CreateAsync(AttachmentFileModel model)
    {
        var entity = mapper.Map<AttachmentFileEntity>(model);
        return mapper.Map<AttachmentFileModel>(await repository.CreateAsync(entity));
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

    private async Task CreateFileInS3(IFormFile formFile, string objectPath)
    {
        try
        {
            var bucketExists = await minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(bucketName));
            if (!bucketExists) await minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(bucketName));

            using (var stream = formFile.OpenReadStream())
            {
                await minioClient.PutObjectAsync(new PutObjectArgs()
                    .WithBucket(bucketName)
                    .WithObject(objectPath)
                    .WithStreamData(stream)
                    .WithObjectSize(formFile.Length)
                    .WithContentType(formFile.ContentType));
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private async Task CreateFileInS3(MemoryStream memoryStream, string objectPath, string contentType)
    {
        var bucketExists = await minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(bucketName));
        if (!bucketExists) await minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(bucketName));

        memoryStream.Position = 0;

        await minioClient.PutObjectAsync(new PutObjectArgs()
            .WithBucket(bucketName)
            .WithObject(objectPath)
            .WithStreamData(memoryStream)
            .WithObjectSize(memoryStream.Length)
            .WithContentType(contentType));
    }

    private static string GetShortGuid()
    {
        return Guid.NewGuid().ToString("N")[..6];
    }
}
