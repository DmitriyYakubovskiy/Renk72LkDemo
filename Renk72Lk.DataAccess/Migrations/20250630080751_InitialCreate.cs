using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Renk72Lk.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Index = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    Region = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    City = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Street = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    House = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    Build = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    Office = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_addresses", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "aspnet_roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aspnet_roles", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "attachment_files",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    FileName = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    FilePath = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_attachment_files", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "aspnet_role_claims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aspnet_role_claims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_aspnet_role_claims_aspnet_roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "aspnet_roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Surname = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Patronymic = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Snils = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    PassportType = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: true),
                    PassportSeries = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    PassportNumber = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    PassportDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    PassportIssuedBy = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    PlaceOfBirth = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UserDataAgreementFileId = table.Column<int>(type: "int", nullable: true),
                    ActualAddressId = table.Column<int>(type: "int", nullable: true),
                    RegistrationAddressId = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: true),
                    SecurityStamp = table.Column<string>(type: "longtext", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true),
                    PhoneNumber = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_users_addresses_ActualAddressId",
                        column: x => x.ActualAddressId,
                        principalTable: "addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_users_addresses_RegistrationAddressId",
                        column: x => x.RegistrationAddressId,
                        principalTable: "addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_users_attachment_files_UserDataAgreementFileId",
                        column: x => x.UserDataAgreementFileId,
                        principalTable: "attachment_files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "aspnet_user_claims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aspnet_user_claims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_aspnet_user_claims_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "aspnet_user_logins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false),
                    ProviderKey = table.Column<string>(type: "varchar(255)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "longtext", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aspnet_user_logins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_aspnet_user_logins_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "aspnet_user_roles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aspnet_user_roles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_aspnet_user_roles_aspnet_roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "aspnet_roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_aspnet_user_roles_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "aspnet_user_tokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false),
                    Value = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aspnet_user_tokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_aspnet_user_tokens_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "auth_history",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    LoginIp = table.Column<string>(type: "longtext", nullable: false),
                    LoginDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_auth_history", x => x.Id);
                    table.ForeignKey(
                        name: "FK_auth_history_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "bids",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserRole = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    Name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    Department = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    Service = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    TicketStatus = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    IsArchive = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DocumentFileId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bids", x => x.Id);
                    table.ForeignKey(
                        name: "FK_bids_attachment_files_DocumentFileId",
                        column: x => x.DocumentFileId,
                        principalTable: "attachment_files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_bids_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "bids_attachments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    IsAgreePreviewPDF = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    PassportFileId = table.Column<int>(type: "int", nullable: true),
                    PowerDevicesPlanFileId = table.Column<int>(type: "int", nullable: true),
                    SnilsFileId = table.Column<int>(type: "int", nullable: true),
                    BenefitFileId = table.Column<int>(type: "int", nullable: true),
                    OtherFileId = table.Column<int>(type: "int", nullable: true),
                    BidId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bids_attachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_bids_attachments_attachment_files_BenefitFileId",
                        column: x => x.BenefitFileId,
                        principalTable: "attachment_files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_bids_attachments_attachment_files_OtherFileId",
                        column: x => x.OtherFileId,
                        principalTable: "attachment_files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_bids_attachments_attachment_files_PassportFileId",
                        column: x => x.PassportFileId,
                        principalTable: "attachment_files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_bids_attachments_attachment_files_PowerDevicesPlanFileId",
                        column: x => x.PowerDevicesPlanFileId,
                        principalTable: "attachment_files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_bids_attachments_attachment_files_SnilsFileId",
                        column: x => x.SnilsFileId,
                        principalTable: "attachment_files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_bids_attachments_bids_BidId",
                        column: x => x.BidId,
                        principalTable: "bids",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_bids_attachments_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "bids_connection_object_info",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Region = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    District = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    AddressOfObject = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    CadastralNumber = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    ReasonForBid = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    GuarantySupplier = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    TypeOfContract = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    VoltageClass = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    ConnectionType = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    PowerDevice = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    ObjectName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    BidId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bids_connection_object_info", x => x.Id);
                    table.ForeignKey(
                        name: "FK_bids_connection_object_info_bids_BidId",
                        column: x => x.BidId,
                        principalTable: "bids",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_bids_connection_object_info_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "bids_personal_info",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Surname = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Patronymic = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Snils = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    PlaceOfBirth = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    PhoneNumber = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    PassportType = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: true),
                    PassportSeries = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    PassportNumber = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    PassportDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    PassportIssuedBy = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    IsAgreePersonData = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ActualAddressId = table.Column<int>(type: "int", nullable: true),
                    RegistrationAddressId = table.Column<int>(type: "int", nullable: true),
                    BidId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bids_personal_info", x => x.Id);
                    table.ForeignKey(
                        name: "FK_bids_personal_info_addresses_ActualAddressId",
                        column: x => x.ActualAddressId,
                        principalTable: "addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_bids_personal_info_addresses_RegistrationAddressId",
                        column: x => x.RegistrationAddressId,
                        principalTable: "addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_bids_personal_info_bids_BidId",
                        column: x => x.BidId,
                        principalTable: "bids",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_bids_personal_info_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "bids_representative_info",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Surname = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Patronymic = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Attorney = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    PhoneNumber = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    PassportType = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: true),
                    PassportSeries = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    PassportNumber = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    PassportDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    PassportIssuedBy = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    BidId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bids_representative_info", x => x.Id);
                    table.ForeignKey(
                        name: "FK_bids_representative_info_bids_BidId",
                        column: x => x.BidId,
                        principalTable: "bids",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_bids_representative_info_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "bids_technical_specifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ReliabilityCategory = table.Column<string>(type: "longtext", nullable: true),
                    OldPointPower = table.Column<string>(type: "longtext", nullable: true),
                    OldPointVolt = table.Column<string>(type: "longtext", nullable: true),
                    CountOfTransformers = table.Column<string>(type: "longtext", nullable: true),
                    TransformersPower = table.Column<string>(type: "longtext", nullable: true),
                    CountOfGenerators = table.Column<string>(type: "longtext", nullable: true),
                    GeneratorsPower = table.Column<string>(type: "longtext", nullable: true),
                    TypeOfLoad = table.Column<string>(type: "longtext", nullable: true),
                    TechMin = table.Column<string>(type: "longtext", nullable: true),
                    JustificationTechMin = table.Column<string>(type: "longtext", nullable: true),
                    NatureLoad = table.Column<string>(type: "longtext", nullable: true),
                    PaymentOrder = table.Column<string>(type: "longtext", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    BidId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bids_technical_specifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_bids_technical_specifications_bids_BidId",
                        column: x => x.BidId,
                        principalTable: "bids",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_bids_technical_specifications_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Message = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    FileId = table.Column<int>(type: "int", nullable: true),
                    BidId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_messages_attachment_files_FileId",
                        column: x => x.FileId,
                        principalTable: "attachment_files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_messages_bids_BidId",
                        column: x => x.BidId,
                        principalTable: "bids",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_messages_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "attachments_points",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Voltage = table.Column<float>(type: "float", nullable: true),
                    Power = table.Column<float>(type: "float", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TechnicalSpecificationsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_attachments_points", x => x.Id);
                    table.ForeignKey(
                        name: "FK_attachments_points_bids_technical_specifications_TechnicalSp~",
                        column: x => x.TechnicalSpecificationsId,
                        principalTable: "bids_technical_specifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "attachments_stages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    DesignPeriod = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CommissioningPeriod = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Power = table.Column<float>(type: "float", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TechnicalSpecificationsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_attachments_stages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_attachments_stages_bids_technical_specifications_TechnicalSp~",
                        column: x => x.TechnicalSpecificationsId,
                        principalTable: "bids_technical_specifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_aspnet_role_claims_RoleId",
                table: "aspnet_role_claims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "aspnet_roles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_aspnet_user_claims_UserId",
                table: "aspnet_user_claims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_aspnet_user_logins_UserId",
                table: "aspnet_user_logins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_aspnet_user_roles_RoleId",
                table: "aspnet_user_roles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_attachments_points_TechnicalSpecificationsId",
                table: "attachments_points",
                column: "TechnicalSpecificationsId");

            migrationBuilder.CreateIndex(
                name: "IX_attachments_stages_TechnicalSpecificationsId",
                table: "attachments_stages",
                column: "TechnicalSpecificationsId");

            migrationBuilder.CreateIndex(
                name: "IX_auth_history_UserId",
                table: "auth_history",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_bids_DocumentFileId",
                table: "bids",
                column: "DocumentFileId");

            migrationBuilder.CreateIndex(
                name: "IX_bids_UserId",
                table: "bids",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_bids_attachments_BenefitFileId",
                table: "bids_attachments",
                column: "BenefitFileId");

            migrationBuilder.CreateIndex(
                name: "IX_bids_attachments_BidId",
                table: "bids_attachments",
                column: "BidId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_bids_attachments_OtherFileId",
                table: "bids_attachments",
                column: "OtherFileId");

            migrationBuilder.CreateIndex(
                name: "IX_bids_attachments_PassportFileId",
                table: "bids_attachments",
                column: "PassportFileId");

            migrationBuilder.CreateIndex(
                name: "IX_bids_attachments_PowerDevicesPlanFileId",
                table: "bids_attachments",
                column: "PowerDevicesPlanFileId");

            migrationBuilder.CreateIndex(
                name: "IX_bids_attachments_SnilsFileId",
                table: "bids_attachments",
                column: "SnilsFileId");

            migrationBuilder.CreateIndex(
                name: "IX_bids_attachments_UserId",
                table: "bids_attachments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_bids_connection_object_info_BidId",
                table: "bids_connection_object_info",
                column: "BidId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_bids_connection_object_info_UserId",
                table: "bids_connection_object_info",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_bids_personal_info_ActualAddressId",
                table: "bids_personal_info",
                column: "ActualAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_bids_personal_info_BidId",
                table: "bids_personal_info",
                column: "BidId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_bids_personal_info_RegistrationAddressId",
                table: "bids_personal_info",
                column: "RegistrationAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_bids_personal_info_UserId",
                table: "bids_personal_info",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_bids_representative_info_BidId",
                table: "bids_representative_info",
                column: "BidId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_bids_representative_info_UserId",
                table: "bids_representative_info",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_bids_technical_specifications_BidId",
                table: "bids_technical_specifications",
                column: "BidId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_bids_technical_specifications_UserId",
                table: "bids_technical_specifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_messages_BidId",
                table: "messages",
                column: "BidId");

            migrationBuilder.CreateIndex(
                name: "IX_messages_FileId",
                table: "messages",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_messages_UserId",
                table: "messages",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_users_ActualAddressId",
                table: "users",
                column: "ActualAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_users_RegistrationAddressId",
                table: "users",
                column: "RegistrationAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_users_UserDataAgreementFileId",
                table: "users",
                column: "UserDataAgreementFileId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "users",
                column: "NormalizedUserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "aspnet_role_claims");

            migrationBuilder.DropTable(
                name: "aspnet_user_claims");

            migrationBuilder.DropTable(
                name: "aspnet_user_logins");

            migrationBuilder.DropTable(
                name: "aspnet_user_roles");

            migrationBuilder.DropTable(
                name: "aspnet_user_tokens");

            migrationBuilder.DropTable(
                name: "attachments_points");

            migrationBuilder.DropTable(
                name: "attachments_stages");

            migrationBuilder.DropTable(
                name: "auth_history");

            migrationBuilder.DropTable(
                name: "bids_attachments");

            migrationBuilder.DropTable(
                name: "bids_connection_object_info");

            migrationBuilder.DropTable(
                name: "bids_personal_info");

            migrationBuilder.DropTable(
                name: "bids_representative_info");

            migrationBuilder.DropTable(
                name: "messages");

            migrationBuilder.DropTable(
                name: "aspnet_roles");

            migrationBuilder.DropTable(
                name: "bids_technical_specifications");

            migrationBuilder.DropTable(
                name: "bids");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "addresses");

            migrationBuilder.DropTable(
                name: "attachment_files");
        }
    }
}
