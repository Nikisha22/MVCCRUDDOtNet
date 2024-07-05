using Microsoft.EntityFrameworkCore;
using PracticeMVCProject.Models;
using PracticeMVCProject.Security;

namespace PracticeMVCProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<MvcfirstProjectDatabaseContext>(o => o.UseSqlServer(builder.Configuration["Conn"]));
            builder.Services.AddSingleton<DataSecurityProvider>();

            var app = builder.Build();
            app.UseStaticFiles();
            app.UseRouting();
            app.MapControllerRoute(
                name:"default",
                pattern:"{Controller=Home}/{Action=index}/{id?}"
                );

/*            app.MapGet("/", () => "Hello World!");
*/
            app.Run();
        }
    }
}
