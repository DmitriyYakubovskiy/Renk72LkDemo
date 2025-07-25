using Renk72Lk;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

CreateHostBuilder(args).Build().Run();

static IHostBuilder CreateHostBuilder(string[] args)
    => Host.CreateDefaultBuilder(args).UseSerilog().ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
