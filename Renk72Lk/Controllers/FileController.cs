using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Minio.DataModel.Args;
using Minio;
using Microsoft.Extensions.Options;
using Renk72Lk.Settings;

namespace Renk72Lk.Controllers;

[Route("Uploads")]
[ApiController]
[Authorize("NotBannedPolicy")]
[ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any)]
public class FileController : Controller
{
    private readonly IMinioClient minioClient;
    private string bucketName;

    public FileController(IMinioClient minioClient, IOptions<MinioSettings> minioSettings)
    {
        this.minioClient = minioClient;
        bucketName = minioSettings.Value.BucketName;
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
            Console.WriteLine(ex.Message);
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
