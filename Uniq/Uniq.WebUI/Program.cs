using Uniq.BL.Repositories;
using Uniq.DAL.Contexts;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddScoped(typeof(IRepository<>), typeof(SqlRepository<>));
builder.Services.AddDbContext<SqlContext>(options =>
          options.UseSqlServer(builder.Configuration.GetConnectionString("CS1")));


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "UniqAdminAuth";
    options.DefaultSignInScheme = "UniqAdminAuth";
    options.DefaultChallengeScheme = "UniqAdminAuth";
}).AddCookie("UniqAdminAuth", opt =>
    {
        opt.ExpireTimeSpan = TimeSpan.FromMinutes(60);
        opt.LoginPath = "/admin";
        opt.LogoutPath = "/admin/logout";
    });


var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseStatusCodePagesWithRedirects("/hata/{0}");
    //hata olursa hata sayfasýna yönlendiricez hata sayfasý hazýrlamayý unutma
}
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication(); //kimlik doðrulama
app.UseAuthorization(); //kimlik yetkilendirme
app.MapControllerRoute(name: "admin", pattern: "{area:exists}/{controller=home}/{action=index}/{id?}");
app.MapControllerRoute(name: "default", pattern: "{controller=home}/{action=index}/{id?}");


app.Run();
