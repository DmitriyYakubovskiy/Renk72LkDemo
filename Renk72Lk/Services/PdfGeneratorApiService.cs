using Microsoft.Extensions.Options;
using Renk72Lk.Settings;
using System.Diagnostics;

namespace Renk72Lk.Services;

public class PdfGeneratorApiService : IHostedService
{
    private readonly PdfGeneratorApiSettings apiSettings;
    private Process? apiOpenProcess;

    public PdfGeneratorApiService(IOptions<PdfGeneratorApiSettings> apiSettings)
    {
        this.apiSettings = apiSettings.Value;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        apiOpenProcess = StartApiServer();
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        if (apiOpenProcess != null && !apiOpenProcess.HasExited)
        {
            var process = ExecuteApiCommand("server:shutdown");
            process.WaitForExit(2000);

            if (!process.HasExited)
            {
                process.Kill();
            }
            if (!apiOpenProcess.HasExited)
            {
                apiOpenProcess.Kill();
            }
            process.Dispose();
            apiOpenProcess.Dispose();
        }
        return Task.CompletedTask;
    }

    private Process StartApiServer()
    {
        string apiPath = Path.GetFullPath(apiSettings.Path);
        string host = apiSettings.Host;
        int port = apiSettings.Port;

        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = "php",
            Arguments = $"artisan serve --host={host} --port={port}",
            WorkingDirectory = apiPath,
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        var process = new Process { StartInfo = startInfo };
        process.EnableRaisingEvents = true;

        process.Exited += (sender, e) =>
        {
        };

        process.Start();

        return process;
    }

    private Process ExecuteApiCommand(string command)
    {
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = "php",
            Arguments = $"artisan {command}",
            WorkingDirectory = Path.GetFullPath(apiSettings.Path),
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        var process = new Process { StartInfo = startInfo };
        process.EnableRaisingEvents = true;

        process.Exited += (sender, e) =>
        {
        };

        process.Start();

        return process;
    }
}
