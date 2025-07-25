using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Minio.DataModel.Args;
using Minio;
using Microsoft.Extensions.Options;
using Renk72Lk.Settings;
using Renk72Lk.Services.DataBase;
using Microsoft.Extensions.Logging;

namespace Renk72Lk.Controllers;

[Route("Uploads")]
[ApiController]
[Authorize("NotBannedPolicy")]
[ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any)]
public class FileController : Controller
{
    private readonly IMinioClient minioClient;
    private readonly IFileService fileService;
    private readonly ILogger<FileController> logger;
    private string bucketName;

    public FileController(IMinioClient minioClient, IFileService fileService, IOptions<MinioSettings> minioSettings, ILogger<FileController> logger)
    {
        this.minioClient = minioClient;
        this.fileService = fileService;
        bucketName = minioSettings.Value.BucketName;
        this.logger = logger;
    }

    [HttpPost("Upload")]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        try
        {
            var fileModel = await fileService.CreateMessageFileAsync(file);

            return Ok(new
            {
                id = fileModel.Id,
                fileName = fileModel.FileName,
                filePath = fileModel.FilePath,

            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpGet("{*path}")]
    public async Task<IActionResult> GetFile(string path)
    {
        try
        {
            var memoryStream = new MemoryStream();

            var args = new GetObjectArgs()
                .WithBucket(bucketName)
                .WithObject(path)
                .WithCallbackStream(async (stream, cancellationToken) =>
                {
                    await stream.CopyToAsync(memoryStream, cancellationToken);
                    memoryStream.Position = 0;
                });

            await minioClient.GetObjectAsync(args);

            var contentType = GetMimeType(Path.GetExtension(path));

            return File(memoryStream, contentType);
        }
        catch (Exception ex)
        {
            logger.LogInformation($"Ошибка получения файла: {ex.Message}");
            return NotFound();
        }
    }

    private static string GetMimeType(string fileExtension) => fileExtension.ToLower() switch
    {
        ".png" => "image/png",
        ".jpg" or ".jpeg" => "image/jpeg",
        ".pdf" => "application/pdf",
        ".txt" => "text/plain",
        _ => "application/octet-stream"
    };
}
