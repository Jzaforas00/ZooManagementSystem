using Microsoft.EntityFrameworkCore;
using ZooManagementSystem.Data.Repositories;
using ZooManagementSystem.Services.Implementations;
using ZooManagementSystem.Services.Interfaces;

namespace ZooManagementSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Add a database connection
            builder.Services.AddDbContext<Data.ZooDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IAnimalQueryService, AnimalQueryService>();
            builder.Services.AddScoped<ICompatibilityService, CompatibilityService>();
            builder.Services.AddScoped<IFeedingService, FeedingService>();
            builder.Services.AddScoped<IManagementService, ManagementService>();
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
