using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using FoodDeliveryWebsite.Models;
using FoodDeliveryWebsite.Models.Common;
using FoodDeliveryWebsite.Models.Dtos.UserDtos;
using FoodDeliveryWebsite.Models.Entities;
using FoodDeliveryWebsite.Models.Enums;
using FoodDeliveryWebsite.Models.Validations;
using FoodDeliveryWebsite.Services;

namespace FoodDeliveryWebsite.UnitTests
{
    public class BaseServiceTests : IDisposable
    {
        protected BaseServiceTests()
        {
            var services = this.SetServices();

            this.ServiceProvider = services.BuildServiceProvider();
            this.DbContext = this.ServiceProvider.GetRequiredService<FoodDeliveryWebsiteDbContext>();
        }

        protected IServiceProvider ServiceProvider { get; set; }

        protected FoodDeliveryWebsiteDbContext DbContext { get; set; }

        public void Dispose()
        {
            this.DbContext.Database.EnsureDeleted();
            this.SetServices();
        }

        protected async Task PopulateDB()
        {
            // TotalUsers: 4, DeletedUsers: 1
            await this.PopulateUsers();

            // TotalAddresses: 3, DeletedAddresses: 1
            await this.PopulateAddresses();

            // TotalDiscounts: 4, AvailableDiscounts: 2, UpcomingDiscounts: 1
            await this.PopulateDiscounts();

            // TotalProducts: 9, UnavailableProducts: 1, DeletedProducts: 1
            await this.PopulateProducts();

            // TotalOrders: 3
            await this.PopulateOrders();

            // TotalOrderItems: 7, OrderItemsWithNullOrder: 1
            await this.PopulateOrderItems();
        }

        protected async Task PopulateUsers()
        {
            // AdminPassword = StrongPassword?555
            await this.DbContext.Users.AddAsync(new User
            {
                Id = 1,
                CreateDate = new DateTime(2023, 11, 2, 8, 20, 50, DateTimeKind.Utc),
                CreatorUserId = 0,
                FirstName = "Админ",
                LastName = "Админ",
                Gender = Gender.Male,
                Email = "admin@gmail.com",
                Password = "41C396CABA416A55EF199EEF5BF18F2485378DD373764A50662B0BE863A218C107E5E3A7305A83A4EBD8A4F7493A79878A8F25F69057896D2CA1477214B31754",
                PhoneNumber = "+359 99 9999 999",
                IsDeleted = false,
                Role = UserRole.Admin,
                Salt = "8EB89F1556CB153CC0D048D70D3B59E03046A1B33EE056350BC3B965617FF54E6EE89B8E64DE705A05B49CDB80B4888BEF78DA7E4FD47FD20AC1EEADD238C8CE"
            });

            // FirstClientPassword = parolaB!123
            await this.DbContext.Users.AddAsync(new User
            {
                Id = 2,
                CreateDate = new DateTime(2024, 1, 13, 14, 4, 39, DateTimeKind.Utc),
                CreatorUserId = 0,
                FirstName = "Иван",
                LastName = "Иванов",
                Gender = Gender.Male,
                Email = "ivan@gmail.com",
                Password = "5E79B341F17FCA086417D1CDBB0419F3F5ADD01D0908A0B331BA5BED403209DADD32DFD8533C56BF2394492D8923DC0B5ED6504803344AA804971B234DA294A8",
                PhoneNumber = "+359 88 8888 888",
                IsDeleted = false,
                Role = UserRole.Client,
                Salt = "C0BFA2CEC43F2B8D90308BB9433B5DE0D5E74B36866DB81253F588AF8ED2815BC801CAF40EB2E4B10A1775D874602097B7358A8E10ABD9C35BEB87A0DFFAD3C6"
            });

            // SecondClientPassword = randomPassword.1234
            await this.DbContext.Users.AddAsync(new User
            {
                Id = 3,
                CreateDate = new DateTime(2024, 2, 14, 10, 21, 18, DateTimeKind.Utc),
                CreatorUserId = 0,
                FirstName = "Мария",
                LastName = "Петрова",
                Gender = Gender.Female,
                Email = "maria@abv.bg",
                Password = "371E67B6847BA48915A2E37C3E1E98F3D8EEFE024EA0CD9C2DAA328743D3E4A87D5F746B80719F249C91311026438CFDA0A83718FEF8E0ED63924334325B8632",
                PhoneNumber = "+359 77 7777 777",
                IsDeleted = false,
                Role = UserRole.Client,
                Salt = "2F785CBCB2BE890D21DA615BEC80A0C3B76C6DD8DC5621F5B7F12A6C668E2883819B470D4603B83295AC5BCBF7393394487199C28484051659B74EA71F3FA26E"
            });

            // ThirdPassword = randomPassword.1234
            await this.DbContext.Users.AddAsync(new User
            {
                Id = 4,
                CreateDate = new DateTime(2024, 2, 18, 12, 21, 18, DateTimeKind.Utc),
                CreatorUserId = 0,
                FirstName = "Владимир",
                LastName = "Стефанов",
                Gender = Gender.Male,
                Email = "vladi@abv.bg",
                Password = "371E67B6847BA48915A2E37C3E1E98F3D8EEFE024EA0CD9C2DAA328743D3E4A87D5F746B80719F249C91311026438CFDA0A83718FEF8E0ED63924334325B8632",
                PhoneNumber = "+359 22 2222 222",
                IsDeleted = true,
                Role = UserRole.Client,
                Salt = "2F785CBCB2BE890D21DA615BEC80A0C3B76C6DD8DC5621F5B7F12A6C668E2883819B470D4603B83295AC5BCBF7393394487199C28484051659B74EA71F3FA26E"
            });

            await this.DbContext.SaveChangesAsync();
        }

        protected async Task PopulateAddresses()
        {
            await this.DbContext.Addresses.AddAsync(new Address
            {
                Id = 1,
                CreateDate = new DateTime(2024, 1, 14, 11, 26, 26, DateTimeKind.Utc),
                CreatorUserId = 2,
                City = "София",
                Street = "Витоша",
                StreetNo = 91,
                Floor = 2,
                UserId = 2,
                IsDeleted = false
            });

            await this.DbContext.Addresses.AddAsync(new Address
            {
                Id = 2,
                CreateDate = new DateTime(2024, 3, 3, 12, 12, 12, DateTimeKind.Utc),
                CreatorUserId = 2,
                City = "Банкя",
                Street = "Христо Ботев",
                StreetNo = 28,
                Floor = 5,
                ApartmentNo = 6,
                UserId = 2,
                IsDeleted = true
            });

            await this.DbContext.Addresses.AddAsync(new Address
            {
                Id = 3,
                CreateDate = new DateTime(2024, 4, 1, 21, 18, 17, DateTimeKind.Utc),
                CreatorUserId = 3,
                City = "Бургас",
                Street = "Славянска",
                StreetNo = 13,
                UserId = 3,
                IsDeleted = false
            });

            await this.DbContext.SaveChangesAsync();
        }

        protected async Task PopulateDiscounts()
        {
            await this.DbContext.Discounts.AddAsync(new Discount
            {
                Id = 1,
                CreateDate = new DateTime(2023, 12, 15, 17, 21, 35, DateTimeKind.Utc),
                CreatorUserId = 1,
                Code = "special8",
                StartDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                ExpirationDate = new DateTime(2024, 1, 2, 23, 59, 59, DateTimeKind.Utc),
                Percentage = 8
            });

            await this.DbContext.Discounts.AddAsync(new Discount
            {
                Id = 2,
                CreateDate = new DateTime(2023, 12, 15, 17, 21, 35, DateTimeKind.Utc),
                CreatorUserId = 1,
                Code = "year2024",
                StartDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                ExpirationDate = new DateTime(2024, 12, 31, 23, 59, 59, DateTimeKind.Utc),
                Percentage = 24
            });

            await this.DbContext.Discounts.AddAsync(new Discount
            {
                Id = 3,
                CreateDate = new DateTime(2023, 12, 17, 2, 5, 47, DateTimeKind.Utc),
                CreatorUserId = 1,
                Code = "summer20",
                StartDate = new DateTime(2024, 6, 1, 0, 0, 0, DateTimeKind.Utc),
                ExpirationDate = new DateTime(2024, 8, 31, 23, 59, 59, DateTimeKind.Utc),
                Percentage = 20
            });

            await this.DbContext.Discounts.AddAsync(new Discount
            {
                Id = 4,
                CreateDate = new DateTime(2023, 12, 18, 2, 5, 47, DateTimeKind.Utc),
                CreatorUserId = 1,
                Code = "april10",
                StartDate = new DateTime(2024, 4, 1, 0, 0, 0, DateTimeKind.Utc),
                ExpirationDate = new DateTime(2024, 4, 30, 23, 59, 59, DateTimeKind.Utc),
                Percentage = 10
            });

            await this.DbContext.SaveChangesAsync();
        }

        protected async Task PopulateProducts()
        {
            //string rootDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
            //string imagesPath = Path.Combine(rootDirectory, "wwwroot", "ProductImages");

            //if (!Directory.Exists(imagesPath))
            //{
            //    throw new DirectoryNotFoundException($"Directory '{imagesPath}' not found.");
            //}

            await this.DbContext.Products.AddAsync(new Product
            {
                Id = 1,
                CreateDate = new DateTime(2023, 12, 20, 13, 0, 0, DateTimeKind.Utc),
                CreatorUserId = 1,
                Name = "Италиана",
                Description = "Бейби моцарела, паста коралини, чери домати, бекон, пармезан, микс зелени салати, млечен сос, чесън, магданозено песто и семена.",
                Price = 11.49m,
                Type = ProductType.Salad,
                Status = ProductStatus.Available,
                Grams = 350,
                IsDeleted = false
            });

            await this.DbContext.Products.AddAsync(new Product
            {
                Id = 2,
                CreateDate = new DateTime(2023, 12, 20, 13, 1, 0, DateTimeKind.Utc),
                CreatorUserId = 1,
                Name = "Домашно гуакамоле",
                Description = "Разядка от авокадо, Пико де Гайо салца, халапеньо, фреш лайм, лук, кориандър и тортила чипс.",
                Price = 9.99m,
                Type = ProductType.Starter,
                Status = ProductStatus.Available,
                Grams = 180,
                IsDeleted = false
            });

            await this.DbContext.Products.AddAsync(new Product
            {
                Id = 3,
                CreateDate = new DateTime(2023, 12, 20, 13, 2, 0, DateTimeKind.Utc),
                CreatorUserId = 1,
                Name = "Хрупкаво пиле",
                Description = "Пилешка пържола от бут в хрупкава панировка, със сос Холандез и печени картофи със зелен лук.",
                Price = 14.99m,
                Type = ProductType.Main,
                Status = ProductStatus.Available,
                Grams = 450,
                IsDeleted = false
            });

            await this.DbContext.Products.AddAsync(new Product
            {
                Id = 4,
                CreateDate = new DateTime(2023, 12, 20, 13, 3, 0, DateTimeKind.Utc),
                CreatorUserId = 1,
                Name = "Хрупкави пикантни скариди",
                Description = "Хрупкави пикантни скариди с шрирача чили майонеза.",
                Price = 16.49m,
                Type = ProductType.Seafood,
                Status = ProductStatus.Available,
                Grams = 150,
                IsDeleted = false
            });

            await this.DbContext.Products.AddAsync(new Product
            {
                Id = 5,
                CreateDate = new DateTime(2023, 12, 20, 13, 4, 0, DateTimeKind.Utc),
                CreatorUserId = 1,
                Name = "Чабата",
                Description = "Италиански хляб.",
                Price = 2.99m,
                Type = ProductType.Bread,
                Status = ProductStatus.Available,
                Grams = 80,
                IsDeleted = false
            });

            await this.DbContext.Products.AddAsync(new Product
            {
                Id = 6,
                CreateDate = new DateTime(2023, 12, 20, 13, 5, 0, DateTimeKind.Utc),
                CreatorUserId = 1,
                Name = "Шоколадова торта с Линдт",
                Description = "Торта от фин млечен шоколад и бисквитен блат, поръсена с какао.",
                Price = 6.49m,
                Type = ProductType.Dessert,
                Status = ProductStatus.Available,
                Grams = 100,
                IsDeleted = false
            });

            await this.DbContext.Products.AddAsync(new Product
            {
                Id = 7,
                CreateDate = new DateTime(2023, 12, 20, 13, 6, 0, DateTimeKind.Utc),
                CreatorUserId = 1,
                Name = "Детско меню с крехки карета",
                Description = "Крехки пилешки карета на скара с гарнитура картофено пюре, хрупкави краставички и сос блу чийз.",
                Price = 9.49m,
                Type = ProductType.Children,
                Status = ProductStatus.Available,
                Grams = 220,
                IsDeleted = false
            });

            await this.DbContext.Products.AddAsync(new Product
            {
                Id = 8,
                CreateDate = new DateTime(2023, 12, 20, 13, 7, 0, DateTimeKind.Utc),
                CreatorUserId = 1,
                Name = "Суфле",
                Description = "Суфле с течен център от шоколад.",
                Price = 6.99m,
                Type = ProductType.Dessert,
                Status = ProductStatus.Unavailable,
                Grams = 80,
                IsDeleted = false
            });

            await this.DbContext.Products.AddAsync(new Product
            {
                Id = 9,
                CreateDate = new DateTime(2023, 12, 20, 13, 8, 0, DateTimeKind.Utc),
                CreatorUserId = 1,
                Name = "Крем брюле",
                Description = "Сметанов крем с топинг по избор.",
                Price = 4.99m,
                Type = ProductType.Dessert,
                Status = ProductStatus.Unavailable,
                Grams = 150,
                IsDeleted = true
            });

            await this.DbContext.SaveChangesAsync();
        }

        protected async Task PopulateOrders()
        {
            await this.DbContext.Orders.AddAsync(new Order
            {
                Id = 1,
                CreateDate = new DateTime(2024, 4, 1, 7, 35, 10, DateTimeKind.Utc),
                CreatorUserId = 2,
                UserId = 2,
                DiscountId = null,
                AddressId = 1,
                TotalPrice = 56.46m,
                DeliveryPrice = 7
            });

            await this.DbContext.Orders.AddAsync(new Order
            {
                Id = 2,
                CreateDate = new DateTime(2024, 4, 2, 18, 25, 15, DateTimeKind.Utc),
                CreatorUserId = 3,
                UserId = 3,
                DiscountId = null,
                AddressId = 3,
                TotalPrice = 32.96m,
                DeliveryPrice = 7
            });

            await this.DbContext.Orders.AddAsync(new Order
            {
                Id = 3,
                CreateDate = new DateTime(2024, 4, 3, 16, 23, 17, DateTimeKind.Utc),
                CreatorUserId = 3,
                UserId = 3,
                DiscountId = 3,
                AddressId = 3,
                TotalPrice = 39.36m,
                DeliveryPrice = 7
            });

            await this.DbContext.SaveChangesAsync();
        }

        protected async Task PopulateOrderItems()
        {
            await this.DbContext.OrderItems.AddAsync(new OrderItem
            {
                Id = 1,
                CreateDate = new DateTime(2024, 4, 1, 7, 30, 10, DateTimeKind.Utc),
                CreatorUserId = 0,
                ProductQuantity = 1,
                Price = 16.49m * 1,
                UserId = 2,
                OrderId = 1,
                ProductId = 4
            });

            await this.DbContext.OrderItems.AddAsync(new OrderItem
            {
                Id = 2,
                CreateDate = new DateTime(2024, 4, 1, 7, 31, 10, DateTimeKind.Utc),
                CreatorUserId = 2,
                ProductQuantity = 2,
                Price = 11.49m * 2,
                UserId = 2,
                OrderId = 1,
                ProductId = 1
            });

            await this.DbContext.OrderItems.AddAsync(new OrderItem
            {
                Id = 3,
                CreateDate = new DateTime(2024, 4, 1, 7, 32, 10, DateTimeKind.Utc),
                CreatorUserId = 2,
                ProductQuantity = 1,
                Price = 9.99m * 1,
                UserId = 2,
                OrderId = 1,
                ProductId = 2
            });

            await this.DbContext.OrderItems.AddAsync(new OrderItem
            {
                Id = 4,
                CreateDate = new DateTime(2024, 4, 2, 17, 28, 11, DateTimeKind.Utc),
                CreatorUserId = 3,
                ProductQuantity = 4,
                Price = 6.49m * 4,
                UserId = 3,
                OrderId = 2,
                ProductId = 6
            });

            await this.DbContext.OrderItems.AddAsync(new OrderItem
            {
                Id = 5,
                CreateDate = new DateTime(2024, 4, 3, 16, 22, 11, DateTimeKind.Utc),
                CreatorUserId = 3,
                ProductQuantity = 2,
                Price = 14.99m * 2,
                UserId = 3,
                OrderId = 3,
                ProductId = 3
            });

            await this.DbContext.OrderItems.AddAsync(new OrderItem
            {
                Id = 6,
                CreateDate = new DateTime(2024, 4, 3, 16, 22, 17, DateTimeKind.Utc),
                CreatorUserId = 3,
                ProductQuantity = 2,
                Price = 2.99m * 2,
                UserId = 3,
                OrderId = 3,
                ProductId = 5
            });

            await this.DbContext.OrderItems.AddAsync(new OrderItem
            {
                Id = 7,
                CreateDate = new DateTime(2024, 4, 3, 17, 22, 17, DateTimeKind.Utc),
                CreatorUserId = 2,
                ProductQuantity = 2,
                Price = 16.49m * 2,
                UserId = 2,
                OrderId = null,
                ProductId = 4
            });

            await this.DbContext.SaveChangesAsync();
        }

        private ServiceCollection SetServices()
        {
            var services = new ServiceCollection();

            services.AddDbContext<FoodDeliveryWebsiteDbContext>(
                options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()));

            services
                .AddScoped<IRepository, Repository>()
                .AddScoped<IPassword, Password>()
                .AddScoped<IUserService, UserService>()
                .AddScoped<IAddressService, AddressService>()
                .AddScoped<IDiscountService, DiscountService>()
                .AddScoped<IProductService, ProductService>()
                .AddScoped<IOrderItemService, OrderItemService>()
                .AddScoped<IOrderService, OrderService>()
                .AddScoped<IValidator<UserRegistrationDto>, UserValidator>()
                .AddScoped<IValidator<Address>, AddressValidator>()
                .AddScoped<IValidator<Discount>, DiscountValidator>();

            services.AddAutoMapper(typeof(MappingProfile));

            var context = new DefaultHttpContext();
            services.AddSingleton<IHttpContextAccessor>(new HttpContextAccessor { HttpContext = context });

            return services;
        }
    }
}
