using Renk72Lk;

CreateHostBuilder(args).Build().Run();

static IHostBuilder CreateHostBuilder(string[] args)
    => Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
