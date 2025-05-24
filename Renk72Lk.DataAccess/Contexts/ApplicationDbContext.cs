using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Renk72Lk.DataAccess.Entities;

namespace Renk72Lk.DataAccess.Contexts;

public class ApplicationDbContext : IdentityDbContext<UserEntity, IdentityRole<int>, int>
{
    public override DbSet<UserEntity> Users { get; set; }
    public virtual DbSet<AuthHistoryEntity> AuthHistory { get; set; }
    public virtual DbSet<MessageEntity> Messages { get; set; }

    public DbSet<AttachmentsPointEntity> AttachmentsPoints { get; set; }
    public DbSet<AttachmentsStageEntity> AttachmentsStages { get; set; }
    public DbSet<AttachmentFileEntity> AttachmentFiles { get; set; }
    public DbSet<AddressEntity> Addresses { get; set; }

    public virtual DbSet<BidPersonalInfoEntity> PersonalInfos { get; set; }
    public virtual DbSet<BidRepresentativeInfoEntity> RepresentativeInfos { get; set; }
    public virtual DbSet<BidConnectionObjectInfoEntity> ConnectionObjectInfos { get; set; }
    public virtual DbSet<BidTechnicalSpecificationsEntity> TechnicalSpecifications { get; set; }
    public virtual DbSet<BidAttachmentsEntity> Attachments { get; set; }
    public virtual DbSet<BidEntity> Bids { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<IdentityRoleClaim<int>>().ToTable("aspnet_role_claims");
        modelBuilder.Entity<IdentityRole<int>>().ToTable("aspnet_roles");
        modelBuilder.Entity<IdentityUserRole<int>>().ToTable("aspnet_user_roles");
        modelBuilder.Entity<IdentityUserClaim<int>>().ToTable("aspnet_user_claims");
        modelBuilder.Entity<IdentityUserLogin<int>>().ToTable("aspnet_user_logins");
        modelBuilder.Entity<IdentityUserToken<int>>().ToTable("aspnet_user_tokens");
        modelBuilder.Entity<UserEntity>().ToTable("users");

        modelBuilder.Entity<AttachmentFileEntity>(e =>
        {
            e.ToTable("attachment_files");

            e.Property(e => e.Id);
            e.Property(e => e.FileName).HasMaxLength(255);
            e.Property(e => e.FilePath).HasMaxLength(500);
            e.Property(b => b.CreatedAt);
            e.Property(b => b.UpdatedAt);

            e.HasKey(e => e.Id);
        });

        modelBuilder.Entity<AuthHistoryEntity>(e =>
        {
            e.ToTable("auth_history");

            e.HasKey(e => e.Id);

            e.Property(e => e.Id);
            e.Property(e => e.UserId);
            e.Property(e => e.LoginIp);
            e.Property(e => e.LoginDateTime);
            e.Property(e => e.CreatedAt);
            e.Property(e => e.UpdatedAt);

            e.HasOne<UserEntity>(e => e.User)
                .WithMany(u => u.AuthHistory)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<UserEntity>(e =>
        {
            e.ToTable("users");

            e.Property(e => e.Surname).HasMaxLength(100);
            e.Property(e => e.Name).HasMaxLength(100);
            e.Property(e => e.Patronymic).HasMaxLength(100);
            e.Property(e => e.UserName).HasMaxLength(50);
            e.Property(e => e.PasswordHash);
            e.Property(e => e.PhoneNumber).HasMaxLength(20);
            e.Property(e => e.Email).HasMaxLength(100);
            e.Property(e => e.Snils).HasMaxLength(20);
            e.Property(e => e.PassportType).HasMaxLength(40);
            e.Property(e => e.PassportSeries).HasMaxLength(10);
            e.Property(e => e.PassportNumber).HasMaxLength(10);
            e.Property(e => e.PassportDate);
            e.Property(e => e.PassportIssuedBy).HasMaxLength(500);
            e.Property(e => e.CreatedAt);
            e.Property(e => e.UpdatedAt);
            e.Property(e => e.DateOfBirth);
            e.Property(e => e.PlaceOfBirth).HasMaxLength(500);

            e.HasOne(u => u.UserDataAgreementFile)
             .WithMany()
             .HasForeignKey(u => u.UserDataAgreementFileId)
             .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(u => u.ActualAddress)
                .WithMany()
                .HasForeignKey(u => u.ActualAddressId)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(u => u.RegistrationAddress)
                 .WithMany()
                 .HasForeignKey(u => u.RegistrationAddressId)
                 .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<BidPersonalInfoEntity>(e =>
        {
            e.ToTable("bids_personal_info");

            e.HasKey(b => b.Id);

            e.Property(b => b.UserId);
            e.Property(b => b.BidId);
            e.Property(b => b.IsAgreePersonData).IsRequired(false).HasMaxLength(3);
            e.Property(b => b.Surname).IsRequired(false).HasMaxLength(100);
            e.Property(b => b.Name).IsRequired(false).HasMaxLength(100);
            e.Property(b => b.Patronymic).IsRequired(false).HasMaxLength(100);
            e.Property(b => b.Snils).IsRequired(false).HasMaxLength(20);
            e.Property(b => b.PhoneNumber).IsRequired(false).HasMaxLength(20);
            e.Property(b => b.Email).IsRequired(false).HasMaxLength(100);
            e.Property(b => b.PassportType).IsRequired(false).HasMaxLength(40);
            e.Property(b => b.PassportSeries).IsRequired(false).HasMaxLength(10);
            e.Property(b => b.PassportNumber).IsRequired(false).HasMaxLength(10);
            e.Property(b => b.PassportDate).IsRequired(false);
            e.Property(b => b.PassportIssuedBy).IsRequired(false).HasMaxLength(500);
            e.Property(b => b.DateOfBirth).IsRequired(false);
            e.Property(b => b.PlaceOfBirth).IsRequired(false).HasMaxLength(500);
            e.Property(b => b.CreatedAt);
            e.Property(b => b.UpdatedAt);

            e.HasOne(e => e.Bid)
                .WithOne(u => u.Step1)
                .HasForeignKey<BidPersonalInfoEntity>(e => e.BidId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(e => e.User)
                .WithMany(u => u.PersonalInfos)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(u => u.ActualAddress)
                .WithMany()
                .HasForeignKey(u => u.ActualAddressId)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(u => u.RegistrationAddress)
                 .WithMany()
                 .HasForeignKey(u => u.RegistrationAddressId)
                 .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<AddressEntity>(e =>
        {
            e.ToTable("addresses");

            e.HasKey(a => a.Id);
            e.Property(a => a.Index).IsRequired(false).HasMaxLength(20);
            e.Property(a => a.Region).IsRequired(false).HasMaxLength(100);
            e.Property(a => a.City).IsRequired(false).HasMaxLength(100);
            e.Property(a => a.Street).IsRequired(false).HasMaxLength(100);
            e.Property(a => a.House).IsRequired(false).HasMaxLength(20);
            e.Property(a => a.Build).IsRequired(false).HasMaxLength(20);
            e.Property(a => a.Office).IsRequired(false).HasMaxLength(20);

            e.Property(b => b.CreatedAt);
            e.Property(b => b.UpdatedAt);
        });

        modelBuilder.Entity<BidRepresentativeInfoEntity>(e =>
        {
            e.ToTable("bids_representative_info");

            e.Property(b => b.Id).IsRequired();
            e.Property(b => b.UserId).IsRequired(false);
            e.Property(b => b.BidId).IsRequired(false);
            e.Property(b => b.Surname).IsRequired(false).HasMaxLength(100);
            e.Property(b => b.Name).IsRequired(false).HasMaxLength(100);
            e.Property(b => b.Patronymic).IsRequired(false).HasMaxLength(100);
            e.Property(b => b.PhoneNumber).IsRequired(false).HasMaxLength(20);
            e.Property(b => b.Email).IsRequired(false).HasMaxLength(100);
            e.Property(b => b.Attorney).IsRequired(false).HasMaxLength(500);
            e.Property(b => b.PassportType).IsRequired(false).HasMaxLength(40);
            e.Property(b => b.PassportSeries).IsRequired(false).HasMaxLength(10);
            e.Property(b => b.PassportNumber).IsRequired(false).HasMaxLength(10);
            e.Property(b => b.PassportDate).IsRequired(false);
            e.Property(b => b.PassportIssuedBy).IsRequired(false).HasMaxLength(500);
            e.Property(b => b.CreatedAt).IsRequired(false);
            e.Property(b => b.UpdatedAt).IsRequired(false);

            e.HasOne(e => e.Bid)
                .WithOne(u => u.Step2)
                .HasForeignKey<BidRepresentativeInfoEntity>(e => e.BidId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(e => e.User)
                .WithMany(u => u.RepresentativeInfos)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<BidConnectionObjectInfoEntity>(e =>
        {
            e.ToTable("bids_connection_object_info");

            e.HasKey(b => b.Id);

            e.Property(b => b.UserId).IsRequired(false);
            e.Property(b => b.BidId).IsRequired(false);
            e.Property(b => b.Region).IsRequired(false).HasMaxLength(100);
            e.Property(b => b.District).IsRequired(false).HasMaxLength(100);
            e.Property(b => b.AddressOfObject).IsRequired(false).HasMaxLength(200);
            e.Property(b => b.CadastralNumber).IsRequired(false).HasMaxLength(500);
            e.Property(b => b.ReasonForBid).IsRequired(false).HasMaxLength(100);
            e.Property(b => b.GuarantySupplier).IsRequired(false).HasMaxLength(100);
            e.Property(b => b.TypeOfContract).IsRequired(false).HasMaxLength(100);
            e.Property(b => b.VoltageClass).IsRequired(false).HasMaxLength(50);
            e.Property(b => b.ConnectionType).IsRequired(false).HasMaxLength(50);
            e.Property(b => b.PowerDevice).HasMaxLength(50);
            e.Property(b => b.ObjectName).IsRequired(false).HasMaxLength(100);

            e.Property(b => b.CreatedAt).IsRequired(false);
            e.Property(b => b.UpdatedAt).IsRequired(false);

            e.HasOne(e => e.Bid)
                .WithOne(u => u.Step3)
                .HasForeignKey<BidConnectionObjectInfoEntity>(e => e.BidId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(e => e.User)
                .WithMany(u => u.ConnectionObjectInfos)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<BidTechnicalSpecificationsEntity>(e =>
        {
            e.ToTable("bids_technical_specifications");

            e.HasKey(b => b.Id);

            e.Property(b => b.UserId).IsRequired(false);
            e.Property(b => b.BidId).IsRequired(false);
            e.Property(b => b.ReliabilityCategory).IsRequired(false);
            e.Property(b => b.CountOfTransformers).IsRequired(false);
            e.Property(b => b.TransformersPower).IsRequired(false);
            e.Property(b => b.CountOfGenerators).IsRequired(false);
            e.Property(b => b.GeneratorsPower).IsRequired(false);
            e.Property(b => b.TypeOfLoad).IsRequired(false);

            e.Property(b => b.TechMin).IsRequired(false);
            e.Property(b => b.JustificationTechMin).IsRequired(false);

            e.Property(b => b.OldPointPower).IsRequired(false);
            e.Property(b => b.OldPointVolt).IsRequired(false);

            e.Property(b => b.NatureLoad).IsRequired(false);
            e.Property(b => b.PaymentOrder).IsRequired(false);

            e.Property(b => b.CreatedAt).IsRequired(false);
            e.Property(b => b.UpdatedAt).IsRequired(false);

            e.HasOne(e => e.Bid)
                .WithOne(u => u.Step4)
                .HasForeignKey<BidTechnicalSpecificationsEntity>(e => e.BidId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(e => e.User)
                .WithMany(u => u.TechnicalSpecifications)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasMany(b => b.Points)
                .WithOne(p => p.TechnicalSpecifications)
                .HasForeignKey(p => p.TechnicalSpecificationsId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasMany(b => b.Stages)
                .WithOne(s => s.TechnicalSpecifications)
                .HasForeignKey(s => s.TechnicalSpecificationsId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<AttachmentsPointEntity>(e =>
        {
            e.ToTable("attachments_points");

            e.HasKey(p => p.Id);

            e.Property(p => p.Voltage);
            e.Property(p => p.Power);

            e.Property(p => p.CreatedAt);
            e.Property(p => p.UpdatedAt);

            e.Property(p => p.TechnicalSpecificationsId);
        });

        modelBuilder.Entity<AttachmentsStageEntity>(e =>
        {
            e.ToTable("attachments_stages");

            e.HasKey(s => s.Id);

            e.Property(s => s.DesignPeriod);
            e.Property(s => s.CommissioningPeriod);
            e.Property(s => s.Power);

            e.Property(s => s.CreatedAt);
            e.Property(s => s.UpdatedAt);

            e.Property(s => s.TechnicalSpecificationsId);
        });

        modelBuilder.Entity<BidAttachmentsEntity>(e =>
            {
                e.ToTable("bids_attachments");

                e.HasKey(b => b.Id);
                e.Property(b => b.IsAgreePreviewPDF).IsRequired(false).HasMaxLength(3);
                e.Property(b => b.UserId).IsRequired(false);
                e.Property(b => b.BidId).IsRequired(false);

                e.Property(b => b.CreatedAt).IsRequired(false);
                e.Property(b => b.UpdatedAt).IsRequired(false);

                e.HasOne(b => b.PassportFile)
                .WithMany()
                .HasForeignKey(b => b.PassportFileId)
                .OnDelete(DeleteBehavior.Restrict); 

                e.HasOne(b => b.PowerDevicesPlanFile)
                    .WithMany()
                    .HasForeignKey(b => b.PowerDevicesPlanFileId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(b => b.SnilsFile)
                    .WithMany()
                    .HasForeignKey(b => b.SnilsFileId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(b => b.BenefitFile)
                    .WithMany()
                    .HasForeignKey(b => b.BenefitFileId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(b => b.OtherFile)
                    .WithMany()
                    .HasForeignKey(b => b.OtherFileId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(e => e.Bid)
                    .WithOne(u => u.Step5)
                    .HasForeignKey<BidAttachmentsEntity>(e => e.BidId)
                    .OnDelete(DeleteBehavior.Cascade);

                e.HasOne(e => e.User)
                    .WithMany(u => u.Attachments)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

        modelBuilder.Entity<MessageEntity>(e =>
        {
            e.ToTable("messages");

            e.HasKey(m => m.Id);

            e.Property(m => m.Id);
            e.Property(m => m.UserId).IsRequired(false);
            e.Property(m => m.BidId).IsRequired(false);
            e.Property(m => m.Message).IsRequired(false).HasMaxLength(255);
            e.Property(m => m.Status).IsRequired().HasDefaultValue(0);
            e.Property(m => m.CreatedAt).IsRequired(false);
            e.Property(m => m.UpdatedAt).IsRequired(false);

            e.HasOne(m => m.File)
                .WithMany()
                .HasForeignKey(m => m.FileId)
                .OnDelete(DeleteBehavior.Restrict);
            
            e.HasOne<UserEntity>(m => m.User)
                .WithMany(u => u.Messages)
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(m => m.Bid)
                .WithMany(b => b.Messages)
                .HasForeignKey(m => m.BidId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<AttachmentsStageEntity>(e =>
        {
            e.ToTable("attachments_stages");

            e.Property(t => t.Id);
            e.Property(t => t.Power);
            e.Property(t => t.DesignPeriod);
            e.Property(t => t.CommissioningPeriod);
            e.Property(t => t.CreatedAt);
            e.Property(t => t.UpdatedAt);

            e.HasOne<BidTechnicalSpecificationsEntity>(e => e.TechnicalSpecifications)
                .WithMany(u => u.Stages)
                .HasForeignKey(e => e.TechnicalSpecificationsId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<AttachmentsPointEntity>(e =>
        {
            e.ToTable("attachments_points");

            e.Property(t => t.Id);
            e.Property(t => t.Power);
            e.Property(t => t.Voltage);

            e.Property(t => t.CreatedAt);
            e.Property(t => t.UpdatedAt);

            e.HasOne<BidTechnicalSpecificationsEntity>(e => e.TechnicalSpecifications)
                .WithMany(u => u.Points)
                .HasForeignKey(e => e.TechnicalSpecificationsId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<BidEntity>(e =>
        {
            e.ToTable("bids");

            e.Property(b => b.Id);
            e.Property(b => b.UserId).IsRequired(false).HasMaxLength(255);
            e.Property(b => b.UserRole).IsRequired(false).HasMaxLength(255);
            e.Property(b => b.Name).IsRequired(false).HasMaxLength(255);
            e.Property(b => b.Department).IsRequired(false).HasMaxLength(255);
            e.Property(b => b.Service).IsRequired(false).HasMaxLength(255);

            e.Property(b => b.Status).IsRequired().HasDefaultValue(0);
            e.Property(b => b.TicketStatus).IsRequired().HasDefaultValue(0);
            e.Property(b => b.IsArchive).IsRequired().HasDefaultValue(0);

            e.Property(b => b.CreatedAt).IsRequired(false);
            e.Property(b => b.UpdatedAt).IsRequired(false);

            e.HasOne<UserEntity>(e => e.User)
                .WithMany(u => u.Bids)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(u => u.DocumentFile)
                 .WithMany()
                 .HasForeignKey(u => u.DocumentFileId)
                 .OnDelete(DeleteBehavior.Restrict);
        });
    }
}

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        var configurationBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.Development.json", optional: false);
        
        IConfiguration configuration = configurationBuilder.Build();
        string connectionString = configuration.GetConnectionString("DbConnection")!;
        
        optionsBuilder.UseMySQL(connectionString);

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}