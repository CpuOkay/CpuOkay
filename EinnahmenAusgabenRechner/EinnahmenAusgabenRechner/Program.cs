using Microsoft.EntityFrameworkCore;
namespace EinnahmenAusgabenRechner
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("HaushaltplanungContext") ?? throw new InvalidOperationException("Connection string 'HaushaltplanungContext' not found.");

            builder.Services.AddDbContext<EinnahmenAusgabenRechnerContext>(options => options.UseSqlServer(connectionString));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Eintrag}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
