using BuyZone.Domain;
using BuyZone.Domain.BaseUser;
using BuyZone.Domain.Entities;
using BuyZone.Domain.Entities.Security;
using BuyZone.WAF.Domain.Entities;
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
        builder.Entity<Order>().HasData(
            new
            {
                Id = Guid.Parse("d4444444-0000-4000-8000-000000000001"),
                Number = 1,
                CustomerId = customer1Id,
                ProductId = product1Id,
                DateCreated = new DateTime(2024, 1, 5, 10, 30, 0, DateTimeKind.Utc),
                Price = 1200.00,
                Quantity = 1
            },
            new
            {
                Id = Guid.Parse("d4444444-0000-4000-8000-000000000002"),
                Number = 2,
                CustomerId = customer2Id,
                ProductId = product2Id,
                DateCreated = new DateTime(2024, 2, 14, 16, 20, 0, DateTimeKind.Utc),
                Price = 20.00,
                Quantity = 3
            });

        base.OnModelCreating(builder);
    }

}