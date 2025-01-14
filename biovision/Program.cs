using biovision.Data;
using biovision.Services;
using Microsoft.EntityFrameworkCore;

namespace biovision
{
	public class Program {
		public static void Main(string[] args) {
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();
			builder.Services.AddHttpContextAccessor();
			builder.Services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Azure_SQL_CONNECTIONSTRING")));
			builder.Services.AddScoped<AssetUploadService>();
			builder.Services.AddScoped<AssetFetchService>();
			builder.Services.AddScoped<DeleteEntryService>();
			builder.Services.AddScoped<AuthService>();

			builder.Services.AddDistributedMemoryCache();
			builder.Services.AddSession(options => {
				options.IdleTimeout = TimeSpan.FromMinutes(30);
				options.Cookie.HttpOnly = true;
				options.Cookie.IsEssential = true;
			});


			var app = builder.Build();

			// Configure the HTTP request pipeline.
			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseSession();
			app.UseRouting(); 
			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}
