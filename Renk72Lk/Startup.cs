using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Minio;
using Renk72Lk.Controllers;
using Renk72Lk.DataAccess.Contexts;
using Renk72Lk.DataAccess.Entities;
using Renk72Lk.DataAccess.Enums;
using Renk72Lk.DataAccess.Extensions;
using Renk72Lk.DataAccess.Repositories;
using Renk72Lk.Handlers;
using Renk72Lk.Hubs;
using Renk72Lk.Requirements;
using Renk72Lk.Services;
using Renk72Lk.Services.DataBase;
using Renk72Lk.Services.Email;
using Renk72Lk.Settings;

namespace Renk72Lk;

public class Startup
{
    private readonly IConfiguration configuration;

    public Startup(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        var connectionString = configuration.GetConnectionString("DbConnection");
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySQL(connectionString!));
        services.Configure<ReportingSettings>(configuration.GetSection("ReportingService"));
        services.Configure<EmailSettings>(configuration.GetSection("Email"));
        services.Configure<MinioSettings>(configuration.GetSection("MinIO"));
        services.Configure<RabbitMQSettings>(configuration.GetSection("RabbitMQ"));
        services.Configure<FormOptions>(options =>
        {
            options.MultipartBodyLengthLimit = 10485760;
        });

        //services.AddHostedService<ReportingService>();
        services.AddHostedService<RabbitMQConsumerService>();
        services.AddScoped<IAuthHistoryRepository, AuthHistoryRepository>();
        services.AddScoped<IBidPersonalInfoRepository, BidPersonalInfoRepository>();
        services.AddScoped<IBidRepresentativeInfoRepository, BidRepresentativeInfoRepository>();
        services.AddScoped<IBidConnectionObjectInfoRepository, BidConnectionObjectInfoRepository>();
        services.AddScoped<IBidTechnicalSpecificationsRepository, BidTechnicalSpecificationsRepository>();
        services.AddScoped<IBidAttachmentsRepository, BidAttachmentsRepository>();
        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddScoped<IBidRepository, BidRepository>();
        services.AddScoped<IAddressRepository, AddressRepository>();
        services.AddScoped<IAttachmentFileRepository, AttachmentFileRepository>();
        services.AddScoped<IAttachmentsPointRepository, AttachmentsPointRepository>();
        services.AddScoped<IAttachmentsStageRepository, AttachmentsStageRepository>();

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthHistoryService, AuthHistoryService>();
        services.AddScoped<IBidPersonalInfoService, BidPersonalInfoService>();
        services.AddScoped<IBidRepresentativeInfoService, BidRepresentativeInfoService>();
        services.AddScoped<IBidConnectionObjectInfoService, BidConnectionObjectInfoService>();
        services.AddScoped<IBidTechnicalSpecificationsService, BidTechnicalSpecificationsService>();
        services.AddScoped<IBidAttachmentsService, BidAttachmentsService>();
        services.AddScoped<IMessageService, MessageService>();
        services.AddScoped<IBidService, BidService>();
        services.AddScoped<IBidViewModelService, BidViewModelService>();
        services.AddScoped<IAddressService, AddressService>();
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<IAttachmentsPointService, AttachmentsPointService>();
        services.AddScoped<IAttachmentsStageService, AttachmentsStageService>();
        services.AddScoped<IRabbitMQProducerSerivce, RabbitMQProducerService>();

        services.AddHttpClient<BidAttachmentsController>();
        services.AddHttpClient<BidController>();
        services.AddHttpClient<RabbitMQProducerService>();

        services.AddHttpContextAccessor();
        services.AddSingleton<IMinioClient>(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<MinioSettings>>().Value;
            return new MinioClient()
                .WithEndpoint(settings.Host)
                .WithCredentials(settings.AccessKey, settings.SecretKey)
                .Build();
        });

        services.AddResponseCaching(options =>
        {
            options.MaximumBodySize = 1024 * 1024 * 256;
            options.UseCaseSensitivePaths = true;
        });

        services.AddIdentity<UserEntity, IdentityRole<int>>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 8;
            options.Password.RequiredUniqueChars = 1;
            options.User.AllowedUserNameCharacters = null!;
        }).AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

        services.Configure<SecurityStampValidatorOptions>(options =>
        {
            options.ValidationInterval = TimeSpan.FromMinutes(1);
        });

        services.AddAutoMapper(typeof(Startup).Assembly);

        services.AddRazorPages().AddRazorRuntimeCompilation();

        services.AddSignalR();

        services.Configure<DataProtectionTokenProviderOptions>(options =>
            options.TokenLifespan = TimeSpan.FromHours(4)); //токен восстановления пароля

        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie
        (
            options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromHours(4);
                options.SlidingExpiration = true; 
                options.LoginPath = "/Account/Login"; 
                options.LogoutPath = "/Account/Logout";
            }
        );

        services.AddAuthorization(options =>
        {
            options.AddPolicy("NotBannedPolicy", policy =>
                policy.Requirements.Add(new NotBannedRequirement()));
        });

        services.AddSingleton<IAuthorizationHandler, NotBannedHandler>();

        services.AddSwaggerGen();
        services.AddHttpContextAccessor();
        services.AddMvc();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime, RoleManager<IdentityRole<int>> roleManager)
    {
        CreateRolesAsync(roleManager).Wait();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        else
        {
            app.UseExceptionHandler("/Error"); 
            app.UseStatusCodePagesWithReExecute("/Error/{0}"); 
        }

        app.UseStaticFiles(new StaticFileOptions
        {
            ContentTypeProvider = ConfigureFileExtensions([".sig"])
        });

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHub<ChatHub>("/chatHub");
        });
    }

    private FileExtensionContentTypeProvider ConfigureFileExtensions(string[] extensions)
    {
        var provider = new FileExtensionContentTypeProvider();
        foreach (var extension in extensions)
        {
            provider.Mappings[extension] = "application/octet-stream";
        }

        return provider;
    }

    private static async Task CreateRolesAsync(RoleManager<IdentityRole<int>> roleManager)
    {
        string[] roleNames = { UserRole.Admin.GetDescription(), UserRole.User.GetDescription() };
        
        foreach (var roleName in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                await roleManager.CreateAsync(new IdentityRole<int>(roleName));
            }
        }
    }
}
