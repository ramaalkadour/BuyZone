using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BuyZone.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Discriminator = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: true),
                    Employee_FirstName = table.Column<string>(type: "text", nullable: true),
                    Employee_LastName = table.Column<string>(type: "text", nullable: true),
                    Employee_Status = table.Column<int>(type: "integer", nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlackIPs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Number = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NumberOfRequests = table.Column<long>(type: "bigint", nullable: false),
                    IpAddress = table.Column<string>(type: "text", nullable: false),
                    SqlInjectionAttempts = table.Column<int>(type: "integer", nullable: false),
                    BlockedCount = table.Column<int>(type: "integer", nullable: false),
                    RateLimitViolations = table.Column<int>(type: "integer", nullable: false),
                    SafeRequestCount = table.Column<long>(type: "bigint", nullable: false),
                    FirstSeen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastSeen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlackIPs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IpAddress = table.Column<string>(type: "text", nullable: false),
                    TypeOfAttack = table.Column<int>(type: "integer", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Path = table.Column<string>(type: "text", nullable: false),
                    Request = table.Column<string>(type: "text", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Number = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Number = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("a1111111-0000-4000-8000-000000000001"), null, "Admin", "ADMIN" },
                    { new Guid("a1111111-0000-4000-8000-000000000002"), null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("b2222222-0000-4000-8000-000000000001"), 0, "123 Main St", "d899d946-0716-4bb6-874b-ed090a1d751b", "Customer", "charlie@customer.com", true, "Charlie", "Brown", false, null, "CHARLIE@CUSTOMER.COM", "CHARLIE@CUSTOMER.COM", "hashed_password_here", "1234567890", false, "64e934db-ef77-46dd-bde7-fce93471e1db", 0, false, "charlie@customer.com" },
                    { new Guid("b2222222-0000-4000-8000-000000000002"), 0, "456 Elm St", "1d781832-9e18-4f76-bb70-af1b2b918911", "Customer", "dana@customer.com", true, "Dana", "Smith", false, null, "DANA@CUSTOMER.COM", "DANA@CUSTOMER.COM", "hashed_password_here", "0987654321", false, "4ee0cb4b-ea4b-4dc6-b3f3-3b6fe83ba61d", 0, false, "dana@customer.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "Employee_FirstName", "Employee_LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Employee_Status", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("e3333133-0000-4000-8000-000000000001"), 0, "11ab417a-b373-45b1-8def-022a89cb6888", "Employee", "bob@company.com", true, "Bob", "Walker", false, null, "BOB@COMPANY.COM", "BOB@COMPANY.COM", "", "888777666", false, "2607be9d-6b70-4c21-aeef-3b121ca0698c", 0, false, "bob@company.com" },
                    { new Guid("e3333333-0000-4000-8000-000000000001"), 0, "5cdf3dcf-5e77-4cc7-8116-45012b4c556a", "Employee", "alice@company.com", true, "Alice", "Johnson", false, null, "ALICE@COMPANY.COM", "ALICE@COMPANY.COM", "", "999888777", false, "d61ba78c-d094-4893-8562-f1eab383daf7", 0, false, "alice@company.com" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("c1a1b1c1-0000-4000-8000-000000000001"), "Electronic devices and gadgets", "Electronics" },
                    { new Guid("c1a1b1c1-0000-4000-8000-000000000002"), "Books and literature", "Books" },
                    { new Guid("c1a1b1c1-0000-4000-8000-000000000003"), "Clothing and accessories", "Clothing" }
                });

            migrationBuilder.InsertData(
                table: "Logs",
                columns: new[] { "Id", "DateCreated", "IpAddress", "Path", "Request", "Status", "TypeOfAttack" },
                values: new object[,]
                {
                    { new Guid("0ce814fd-0a59-4a54-bfbe-09b644a0398a"), new DateTime(2025, 7, 12, 15, 11, 24, 245, DateTimeKind.Utc).AddTicks(3614), "192.168.1.62", "/api/products", "POST /api/products", "Blocked", 2 },
                    { new Guid("1b380af9-7c90-4d32-8809-ed4a8cb49249"), new DateTime(2025, 7, 10, 15, 11, 24, 245, DateTimeKind.Utc).AddTicks(3753), "192.168.1.13", "/category/getall", "GET /api/orders", "Accepted", 3 },
                    { new Guid("26d95f2a-b4f7-4153-a322-1ea6e974f13f"), new DateTime(2025, 7, 7, 15, 11, 24, 245, DateTimeKind.Utc).AddTicks(3406), "192.168.1.205", "/api/products", "DELETE /api/orders", "Accepted", 1 },
                    { new Guid("2c8b3255-be42-4352-be61-95a0e696faa9"), new DateTime(2025, 7, 14, 15, 11, 24, 245, DateTimeKind.Utc).AddTicks(3342), "192.168.1.144", "/order/add", "PUT /api/products", "Accepted", 0 },
                    { new Guid("325cd9be-bf5b-425b-bdc7-966202c9f7b0"), new DateTime(2025, 7, 6, 15, 11, 24, 245, DateTimeKind.Utc).AddTicks(3456), "192.168.1.201", "/order/add", "DELETE /api/products", "Accepted", 1 },
                    { new Guid("3465a06b-9a5d-414f-829e-2c1d1f9dba77"), new DateTime(2025, 7, 11, 15, 11, 24, 245, DateTimeKind.Utc).AddTicks(3418), "192.168.1.40", "/category/getall", "DELETE /order/add", "Accepted", 1 },
                    { new Guid("3695ddf7-dddb-4a31-8694-f2d4756b0690"), new DateTime(2025, 7, 12, 15, 11, 24, 245, DateTimeKind.Utc).AddTicks(3252), "192.168.1.245", "/category/getall", "GET /api/products", "Accepted", 0 },
                    { new Guid("3d9c60f5-00b4-4717-acc8-49694c559b73"), new DateTime(2025, 7, 11, 15, 11, 24, 245, DateTimeKind.Utc).AddTicks(3728), "192.168.1.167", "/category/getall", "PUT /category/getall", "Blocked", 3 },
                    { new Guid("3fa8c35f-d50e-47cd-8230-cbfce7af740c"), new DateTime(2025, 7, 9, 15, 11, 24, 245, DateTimeKind.Utc).AddTicks(3277), "192.168.1.150", "/api/products", "DELETE /api/products", "Accepted", 0 },
                    { new Guid("466487d1-6593-40c5-91f5-575d952b9731"), new DateTime(2025, 7, 13, 15, 11, 24, 245, DateTimeKind.Utc).AddTicks(3291), "192.168.1.149", "/category/getall", "POST /api/orders", "Accepted", 0 },
                    { new Guid("4a403a6c-c9bb-4d30-80d1-026767bde8ed"), new DateTime(2025, 7, 9, 15, 11, 24, 245, DateTimeKind.Utc).AddTicks(3357), "192.168.1.215", "/api/products", "POST /category/getall", "Blocked", 1 },
                    { new Guid("4ec4f5f9-46de-40da-9788-4c599bf6333a"), new DateTime(2025, 7, 6, 15, 11, 24, 245, DateTimeKind.Utc).AddTicks(3316), "192.168.1.241", "/category/getall", "POST /api/orders", "Accepted", 0 },
                    { new Guid("5742f827-326e-42d8-a047-01ac0836b53a"), new DateTime(2025, 7, 8, 15, 11, 24, 245, DateTimeKind.Utc).AddTicks(3741), "192.168.1.37", "/api/products", "PUT /api/orders", "Blocked", 3 },
                    { new Guid("67976db2-5652-4f16-b194-3222861a4300"), new DateTime(2025, 7, 10, 15, 11, 24, 245, DateTimeKind.Utc).AddTicks(3531), "192.168.1.28", "/api/products", "PUT /category/getall", "Blocked", 2 },
                    { new Guid("67e8f6f0-5352-4fe0-ab88-dbb99d98ad9f"), new DateTime(2025, 7, 12, 15, 11, 24, 245, DateTimeKind.Utc).AddTicks(3369), "192.168.1.206", "/category/getall", "GET /category/getall", "Blocked", 1 },
                    { new Guid("8b8ef559-db62-453b-87fb-1d0f28fa564c"), new DateTime(2025, 7, 10, 15, 11, 24, 245, DateTimeKind.Utc).AddTicks(3704), "192.168.1.96", "/category/getall", "PUT /api/products", "Accepted", 3 },
                    { new Guid("922ae683-fd6f-4dd1-a16d-6a7a302004c8"), new DateTime(2025, 7, 9, 15, 11, 24, 245, DateTimeKind.Utc).AddTicks(3394), "192.168.1.251", "/api/products", "POST /api/products", "Accepted", 1 },
                    { new Guid("952d7f4e-ae73-4907-9f73-65fb021e2343"), new DateTime(2025, 7, 14, 15, 11, 24, 245, DateTimeKind.Utc).AddTicks(3264), "192.168.1.242", "/api/orders", "DELETE /order/add", "Accepted", 0 },
                    { new Guid("96d7f7cb-644b-4c01-b975-5793c675d44a"), new DateTime(2025, 7, 10, 15, 11, 24, 245, DateTimeKind.Utc).AddTicks(3519), "192.168.1.125", "/order/add", "GET /category/getall", "Blocked", 2 },
                    { new Guid("9c64666d-e044-4e2d-bd92-751b298647de"), new DateTime(2025, 7, 7, 15, 11, 24, 245, DateTimeKind.Utc).AddTicks(3640), "192.168.1.114", "/api/orders", "DELETE /api/products", "Accepted", 3 },
                    { new Guid("9e2fa1cf-5863-4102-bdd6-6c02963b6814"), new DateTime(2025, 7, 8, 15, 11, 24, 245, DateTimeKind.Utc).AddTicks(3430), "192.168.1.192", "/api/products", "DELETE /category/getall", "Accepted", 1 },
                    { new Guid("a2633b95-2813-48b7-bbe8-2621de600947"), new DateTime(2025, 7, 10, 15, 11, 24, 245, DateTimeKind.Utc).AddTicks(3590), "192.168.1.222", "/order/add", "POST /api/products", "Blocked", 2 },
                    { new Guid("a356a3c7-7907-4098-aef9-972186c739d2"), new DateTime(2025, 7, 10, 15, 11, 24, 245, DateTimeKind.Utc).AddTicks(3213), "192.168.1.71", "/order/add", "GET /category/getall", "Blocked", 0 },
                    { new Guid("a3bf2e9f-d423-498a-90b8-833bb5a9cb8b"), new DateTime(2025, 7, 10, 15, 11, 24, 245, DateTimeKind.Utc).AddTicks(3665), "192.168.1.202", "/order/add", "POST /category/getall", "Accepted", 3 },
                    { new Guid("a61fce81-3813-4eb7-853a-06830e92b939"), new DateTime(2025, 7, 9, 15, 11, 24, 245, DateTimeKind.Utc).AddTicks(3239), "192.168.1.245", "/api/orders", "POST /category/getall", "Accepted", 0 },
                    { new Guid("a6faf414-677b-4e52-827a-36159e3787e7"), new DateTime(2025, 7, 9, 15, 11, 24, 245, DateTimeKind.Utc).AddTicks(3468), "192.168.1.206", "/api/orders", "DELETE /api/products", "Accepted", 1 },
                    { new Guid("afcd1e4c-ce66-4337-ac60-3fa32a6959fc"), new DateTime(2025, 7, 7, 15, 11, 24, 245, DateTimeKind.Utc).AddTicks(3304), "192.168.1.28", "/category/getall", "POST /api/products", "Blocked", 0 },
                    { new Guid("b48301c6-9ffd-45f5-97c3-e95b79fbb8ca"), new DateTime(2025, 7, 15, 15, 11, 24, 245, DateTimeKind.Utc).AddTicks(3691), "192.168.1.223", "/order/add", "PUT /order/add", "Accepted", 3 },
                    { new Guid("bc114754-b808-461b-8dae-e8c938470f97"), new DateTime(2025, 7, 13, 15, 11, 24, 245, DateTimeKind.Utc).AddTicks(3494), "192.168.1.10", "/category/getall", "POST /order/add", "Blocked", 2 },
                    { new Guid("c3851274-c28b-4fa7-8d5a-d4eeeb4fdc47"), new DateTime(2025, 7, 12, 15, 11, 24, 245, DateTimeKind.Utc).AddTicks(3653), "192.168.1.15", "/category/getall", "POST /order/add", "Accepted", 3 },
                    { new Guid("c8ab1d33-9ff4-4251-a94e-4ddb6a0e930a"), new DateTime(2025, 7, 13, 15, 11, 24, 245, DateTimeKind.Utc).AddTicks(3382), "192.168.1.45", "/api/products", "PUT /api/orders", "Accepted", 1 },
                    { new Guid("ce41899e-7c3b-4d64-858c-043667c36e05"), new DateTime(2025, 7, 6, 15, 11, 24, 245, DateTimeKind.Utc).AddTicks(3444), "192.168.1.97", "/order/add", "POST /api/products", "Blocked", 1 },
                    { new Guid("cefca09c-5ac9-43cf-a748-a6bcd053f566"), new DateTime(2025, 7, 15, 15, 11, 24, 245, DateTimeKind.Utc).AddTicks(3626), "192.168.1.60", "/api/orders", "DELETE /api/orders", "Accepted", 2 },
                    { new Guid("cfbe8c94-8ef7-4f1c-8562-a43ae53fff97"), new DateTime(2025, 7, 7, 15, 11, 24, 245, DateTimeKind.Utc).AddTicks(3577), "192.168.1.108", "/api/orders", "POST /api/products", "Accepted", 2 },
                    { new Guid("e5a27a3b-1b34-4395-9624-60c2e23900bd"), new DateTime(2025, 7, 12, 15, 11, 24, 245, DateTimeKind.Utc).AddTicks(3482), "192.168.1.219", "/api/products", "POST /api/products", "Accepted", 2 },
                    { new Guid("e68b8e39-b75d-4605-8a96-df611b52d328"), new DateTime(2025, 7, 8, 15, 11, 24, 245, DateTimeKind.Utc).AddTicks(3329), "192.168.1.50", "/category/getall", "DELETE /api/orders", "Blocked", 0 },
                    { new Guid("e8999040-5555-457a-97fc-abfb9cf03ae5"), new DateTime(2025, 7, 12, 15, 11, 24, 245, DateTimeKind.Utc).AddTicks(3602), "192.168.1.98", "/api/orders", "GET /api/products", "Accepted", 2 },
                    { new Guid("ed521cec-80df-4120-b924-b141c056592d"), new DateTime(2025, 7, 12, 15, 11, 24, 245, DateTimeKind.Utc).AddTicks(3506), "192.168.1.121", "/api/orders", "PUT /category/getall", "Accepted", 2 },
                    { new Guid("fb03428e-47ba-43ca-b6b4-03975e3ff1f7"), new DateTime(2025, 7, 9, 15, 11, 24, 245, DateTimeKind.Utc).AddTicks(3679), "192.168.1.188", "/order/add", "PUT /category/getall", "Blocked", 3 },
                    { new Guid("fbc11bf0-f37a-48cf-93a0-79bd38bb87a3"), new DateTime(2025, 7, 7, 15, 11, 24, 245, DateTimeKind.Utc).AddTicks(3716), "192.168.1.75", "/order/add", "GET /category/getall", "Blocked", 3 }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "Name", "Number", "Price" },
                values: new object[,]
                {
                    { new Guid("f1000001-0000-4000-8000-000000000001"), new Guid("c1a1b1c1-0000-4000-8000-000000000001"), "Gaming Laptop", "/images/laptop.png", "Laptop", 1, 1200.0 },
                    { new Guid("f1000002-0000-4000-8000-000000000002"), new Guid("c1a1b1c1-0000-4000-8000-000000000002"), "Bestselling Novel", "/images/novel.png", "Novel", 2, 20.0 }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CustomerId", "DateCreated", "Number", "Price", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { new Guid("187e41f2-82e1-4bb1-ba55-c1342d8ce656"), new Guid("b2222222-0000-4000-8000-000000000001"), new DateTime(2025, 7, 9, 15, 11, 24, 245, DateTimeKind.Utc).AddTicks(2923), 2, 99.75, new Guid("f1000001-0000-4000-8000-000000000001"), 3 },
                    { new Guid("2931c1be-ddf7-4e6c-b85d-4df1a21d8395"), new Guid("b2222222-0000-4000-8000-000000000002"), new DateTime(2025, 7, 10, 15, 11, 24, 245, DateTimeKind.Utc).AddTicks(3057), 15, 574.38999999999999, new Guid("f1000002-0000-4000-8000-000000000002"), 4 },
                    { new Guid("29480091-26fd-48d1-82be-b737bdba2675"), new Guid("b2222222-0000-4000-8000-000000000002"), new DateTime(2025, 7, 7, 15, 11, 24, 245, DateTimeKind.Utc).AddTicks(3017), 11, 607.10000000000002, new Guid("f1000002-0000-4000-8000-000000000002"), 2 },
                    { new Guid("323f872a-2086-4d61-a87e-243f1ccacbe0"), new Guid("b2222222-0000-4000-8000-000000000002"), new DateTime(2025, 7, 10, 15, 11, 24, 245, DateTimeKind.Utc).AddTicks(2954), 5, 919.44000000000005, new Guid("f1000002-0000-4000-8000-000000000002"), 2 },
                    { new Guid("3798dcbc-7a3c-4eb8-bf56-afd3eeba9665"), new Guid("b2222222-0000-4000-8000-000000000001"), new DateTime(2025, 7, 9, 15, 11, 24, 245, DateTimeKind.Utc).AddTicks(2943), 4, 652.66999999999996, new Guid("f1000001-0000-4000-8000-000000000001"), 4 },
                    { new Guid("43b92e8c-dd8d-408a-9121-c7b6037d5dc1"), new Guid("b2222222-0000-4000-8000-000000000002"), new DateTime(2025, 7, 14, 15, 11, 24, 245, DateTimeKind.Utc).AddTicks(3037), 13, 695.71000000000004, new Guid("f1000002-0000-4000-8000-000000000002"), 2 },
                    { new Guid("5008f69d-e143-4954-bc98-d672a1272477"), new Guid("b2222222-0000-4000-8000-000000000001"), new DateTime(2025, 7, 10, 15, 11, 24, 245, DateTimeKind.Utc).AddTicks(3047), 14, 848.58000000000004, new Guid("f1000001-0000-4000-8000-000000000001"), 4 },
                    { new Guid("52454ba9-16a3-4362-a5da-cf0ae00b4915"), new Guid("b2222222-0000-4000-8000-000000000001"), new DateTime(2025, 7, 10, 15, 11, 24, 245, DateTimeKind.Utc).AddTicks(2986), 8, 832.25999999999999, new Guid("f1000001-0000-4000-8000-000000000001"), 3 },
                    { new Guid("52d81af7-f371-45a2-978a-45b084df9c1d"), new Guid("b2222222-0000-4000-8000-000000000001"), new DateTime(2025, 7, 6, 15, 11, 24, 245, DateTimeKind.Utc).AddTicks(3027), 12, 899.13, new Guid("f1000001-0000-4000-8000-000000000001"), 4 },
                    { new Guid("829fe8e2-f135-4e0e-a076-aca633f11d2c"), new Guid("b2222222-0000-4000-8000-000000000002"), new DateTime(2025, 7, 6, 15, 11, 24, 245, DateTimeKind.Utc).AddTicks(2899), 1, 166.00999999999999, new Guid("f1000002-0000-4000-8000-000000000002"), 1 },
                    { new Guid("8e8111d7-26e6-4570-a692-7c12ceb95e1c"), new Guid("b2222222-0000-4000-8000-000000000001"), new DateTime(2025, 7, 7, 15, 11, 24, 245, DateTimeKind.Utc).AddTicks(3007), 10, 387.60000000000002, new Guid("f1000001-0000-4000-8000-000000000001"), 2 },
                    { new Guid("92a82576-600f-48ab-a36a-5e6a143036d9"), new Guid("b2222222-0000-4000-8000-000000000002"), new DateTime(2025, 7, 13, 15, 11, 24, 245, DateTimeKind.Utc).AddTicks(2996), 9, 961.98000000000002, new Guid("f1000002-0000-4000-8000-000000000002"), 3 },
                    { new Guid("a4529113-bc9b-4008-a7a6-d0d9310fc994"), new Guid("b2222222-0000-4000-8000-000000000002"), new DateTime(2025, 7, 11, 15, 11, 24, 245, DateTimeKind.Utc).AddTicks(2976), 7, 623.51999999999998, new Guid("f1000002-0000-4000-8000-000000000002"), 3 },
                    { new Guid("c0c099ae-5858-4db2-b6ac-c621a921904b"), new Guid("b2222222-0000-4000-8000-000000000002"), new DateTime(2025, 7, 11, 15, 11, 24, 245, DateTimeKind.Utc).AddTicks(2933), 3, 335.32999999999998, new Guid("f1000002-0000-4000-8000-000000000002"), 2 },
                    { new Guid("c4de6e46-4904-40fa-be73-1ef31c9efba6"), new Guid("b2222222-0000-4000-8000-000000000001"), new DateTime(2025, 7, 13, 15, 11, 24, 245, DateTimeKind.Utc).AddTicks(2966), 6, 238.52000000000001, new Guid("f1000001-0000-4000-8000-000000000001"), 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ProductId",
                table: "Orders",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CategoryId",
                table: "Product",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BlackIPs");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
