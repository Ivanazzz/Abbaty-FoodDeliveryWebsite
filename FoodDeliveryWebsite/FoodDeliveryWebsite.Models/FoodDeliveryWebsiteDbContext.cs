using System.Security.Claims;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

using FoodDeliveryWebsite.Models.Common;
using FoodDeliveryWebsite.Models.Entities;
using Microsoft.AspNetCore.Http;

namespace FoodDeliveryWebsite.Models
{
    public class FoodDeliveryWebsiteDbContext : DbContext
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public FoodDeliveryWebsiteDbContext(DbContextOptions<FoodDeliveryWebsiteDbContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Discount> Discounts { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                // Configure name mappings
                entity.SetTableName(entity.ClrType.Name.ToLower());
                if (typeof(IEntity).IsAssignableFrom(entity.ClrType))
                {
                    modelBuilder.Entity(entity.ClrType)
                        .HasKey(nameof(IEntity.Id));
                }

                entity.GetProperties()
                    .ToList()
                    .ForEach(e => e.SetColumnName(e.Name.ToLower()));
                entity.GetForeignKeys()
                    .Where(e => !e.IsOwnership && e.DeleteBehavior == DeleteBehavior.Cascade)
                    .ToList()
                    .ForEach(e => e.DeleteBehavior = DeleteBehavior.Restrict);
            }
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FoodDeliveryWebsiteDbContext).Assembly);
        }

        public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            return Database.BeginTransactionAsync(cancellationToken);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var email = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            var user = await this.Users.FirstOrDefaultAsync(u => u.Email == email && u.IsDeleted == false);

            foreach (var entry in ChangeTracker.Entries())
            {
                if (typeof(IAuditable).IsAssignableFrom(entry.Entity.GetType()) && entry.State == EntityState.Added)
                {
                    var entity = entry.Entity as IAuditable;
                    entity.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
                    entity.CreatorUserId = user == null 
                        ? 0 
                        : user.Id;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}