using BuyZone.Domain;
using BuyZone.Domain.BaseUser;
using BuyZone.Domain.Entities;
using BuyZone.Domain.Entities.Security;
using BuyZone.WAF.Domain.Entities;
using BuyZone.WAF.Domain.Enums;
using DefaultNamespace;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BuyZone.Infrastructure.DbContest;

public class BuyZoneDbContext:IdentityDbContext<User,Role,Guid>
{
    public BuyZoneDbContext(DbContextOptions<BuyZoneDbContext> options) : base(options)
    {
    }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<BlockIP> BlackIPs { get; set; }
    public DbSet<Logs>Logs { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Product>()
            .Property(p => p.Number)
            .UseIdentityColumn()
            .ValueGeneratedOnAdd();
        builder.Entity<BlockIP>()
            .Property(p => p.Number)
            .UseIdentityColumn()
            .ValueGeneratedOnAdd();
        builder.Entity<Order>()
            .Property(p => p.Number)
            .UseIdentityColumn()
            .ValueGeneratedOnAdd();

        // Data Seeding for Category
        var electronicsCategoryId = Guid.Parse("c1a1b1c1-0000-4000-8000-000000000001");
        var booksCategoryId = Guid.Parse("c1a1b1c1-0000-4000-8000-000000000002");
        var clothingCategoryId = Guid.Parse("c1a1b1c1-0000-4000-8000-000000000003");
        builder.Entity<Category>().HasData(
            new Category("Electronics", "Electronic devices and gadgets") { Id = electronicsCategoryId },
            new Category("Books", "Books and literature") { Id = booksCategoryId },
            new Category("Clothing", "Clothing and accessories") { Id = clothingCategoryId }
        );

        // Data Seeding for Product
        builder.Entity<Product>().HasData(
            new Product
            {
                Id = Guid.Parse("f1000001-0000-4000-8000-000000000001"),
                Number = 1,
                Name = "Laptop",
                ImageUrl = "/images/laptop.png",
                Description = "Gaming Laptop",
                Price = 1200.0,
                CategoryId = electronicsCategoryId,
            },
            new Product
            {
                Id = Guid.Parse("f1000002-0000-4000-8000-000000000002"),
                Number = 2,
                    Name = "Novel",
                    ImageUrl = "/images/novel.png",
                    Description = "Bestselling Novel",
                    Price = 20.0,
                    CategoryId = booksCategoryId,
                }
            );

            // Data Seeding for Role
        builder.Entity<Role>().HasData(
            new Role
            {
                Id = Guid.Parse("a1111111-0000-4000-8000-000000000001"),
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
            new Role
            {
                Id = Guid.Parse("a1111111-0000-4000-8000-000000000002"),
                Name = "User",
                NormalizedName = "USER"
            }
        );

        // Data Seeding for Customer
        builder.Entity<Customer>().HasData(
            new
            {
                Id = Guid.Parse("b2222222-0000-4000-8000-000000000001"),
                UserName = "charlie@customer.com",
                NormalizedUserName = "CHARLIE@CUSTOMER.COM",
                Email = "charlie@customer.com",
                NormalizedEmail = "CHARLIE@CUSTOMER.COM",
                EmailConfirmed = true,
                PasswordHash = "hashed_password_here", // ضع هنا كلمة المرور المشفرة أو اتركها فارغة ""
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                PhoneNumber = "1234567890",
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = false,
                AccessFailedCount = 0,
                FirstName = "Charlie",
                LastName = "Brown",
                Address = "123 Main St",
                Status = CustomerEnum.Active
            },
            new
            {
                Id = Guid.Parse("b2222222-0000-4000-8000-000000000002"),
                UserName = "dana@customer.com",
                NormalizedUserName = "DANA@CUSTOMER.COM",
                Email = "dana@customer.com",
                NormalizedEmail = "DANA@CUSTOMER.COM",
                EmailConfirmed = true,
                PasswordHash = "hashed_password_here",
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                PhoneNumber = "0987654321",
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = false,
                AccessFailedCount = 0,
                FirstName = "Dana",
                LastName = "Smith",
                Address = "456 Elm St",
                Status = CustomerEnum.Active
            }
        );


        // Data Seeding for Employee
        builder.Entity<Employee>().HasData(
            new
            {
                Id = Guid.Parse("e3333333-0000-4000-8000-000000000001"),
                UserName = "alice@company.com",
                NormalizedUserName = "ALICE@COMPANY.COM",
                Email = "alice@company.com",
                NormalizedEmail = "ALICE@COMPANY.COM",
                EmailConfirmed = true,
                PasswordHash = "", // أو قيمة مشفرة لكلمة مرور
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                PhoneNumber = "999888777",
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = false,
                AccessFailedCount = 0,
                FirstName = "Alice",
                LastName = "Johnson",
                Status = EmployeeStatus.Active
            },
            new
            {
                Id = Guid.Parse("e3333133-0000-4000-8000-000000000001"),
                UserName = "bob@company.com",
                NormalizedUserName = "BOB@COMPANY.COM",
                Email = "bob@company.com",
                NormalizedEmail = "BOB@COMPANY.COM",
                EmailConfirmed = true,
                PasswordHash = "",
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                PhoneNumber = "888777666",
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = false,
                AccessFailedCount = 0,
                FirstName = "Bob",
                LastName = "Walker",
                Status = EmployeeStatus.Active
            }
        );

        var customer1Id = Guid.Parse("b2222222-0000-4000-8000-000000000001");
        var customer2Id = Guid.Parse("b2222222-0000-4000-8000-000000000002");
        var product1Id = Guid.Parse("f1000001-0000-4000-8000-000000000001");
        var product2Id = Guid.Parse("f1000002-0000-4000-8000-000000000002");

        var random1 = new Random();
        var ordersSeedData = new List<object>();

        for (int i = 1; i <= 15; i++)
        {
            var customerId = (i % 2 == 0) ? customer1Id : customer2Id;
            var productId = (i % 2 == 0) ? product1Id : product2Id;

            ordersSeedData.Add(new
            {
                Id = Guid.NewGuid(),
                Number = i,
                CustomerId = customerId,
                ProductId = productId,
                DateCreated = DateTime.UtcNow.AddDays(-random1.Next(1, 10)),
                Price = Math.Round(random1.NextDouble() * 1000 + 50, 2), // أسعار بين 50 و 1050 تقريباً
                Quantity = random1.Next(1, 5) // كمية بين 1 و 4
            });
        }

        builder.Entity<Order>().HasData(ordersSeedData);
        var random = new Random();
        var statuses = new[] { "Blocked", "Accepted" };
        var paths = new[] { "/api/orders", "/api/products", "/order/add", "/category/getall" };
        var sampleRequests = new[] { "GET", "POST", "PUT", "DELETE" };

        var allAttackTypes = Enum.GetValues(typeof(TypeOfAttack));
        var logSeedData = new List<Logs>();
        int idCounter = 1;

        foreach (TypeOfAttack attackType in allAttackTypes)
        {
            for (int i = 0; i < 10; i++)
            {
                logSeedData.Add(new Logs(
                    ipAddress: $"192.168.1.{random.Next(1,255)}",
                    typeOfAttack: attackType,
                    status: statuses[random.Next(statuses.Length)],
                    request: $"{sampleRequests[random.Next(sampleRequests.Length)]} {paths[random.Next(paths.Length)]}",
                    dateCreated: DateTime.UtcNow.AddDays(-random.Next(0, 10))
                )
                {
                    Id = Guid.NewGuid(),
                    Path = paths[random.Next(paths.Length)]
                });
                idCounter++;
            }
        }

        builder.Entity<Logs>().HasData(logSeedData);

        base.OnModelCreating(builder);
    }

}