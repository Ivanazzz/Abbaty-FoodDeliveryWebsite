using Microsoft.EntityFrameworkCore;

using FoodDeliveryWebsite.Models;

namespace FoodDeliveryWebsite
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //builder.Services.AddApplication();
            builder.Services.AddDbContext<FoodDeliveryWebsiteDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DBConnection")));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddApplication();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(options => options
                .AllowAnyHeader()
                .AllowAnyOrigin()
                .AllowAnyMethod());

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
