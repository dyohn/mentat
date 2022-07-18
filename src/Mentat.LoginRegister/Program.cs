using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Mentat.LoginRegister.Areas.Identity.Data;
using Mentat.LoginRegister.Settings;
using Mentat.LoginRegister.Areas.Identity;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
//var connectionString = builder.Configuration.GetConnectionString("AuthDbContextConnection") ?? throw new InvalidOperationException("Connection string 'AuthDbContextConnection' not found.");

//builder.Services.AddDbContext<AuthDbContext>(options => options.UseSqlServer(connectionString));;

ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;

var mongoDbSettings = configuration.GetSection(nameof(MongoDbConfig)).Get<MongoDbConfig>();

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{ options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+/ "; 
  //options.SignIn.RequireConfirmedAccount = true; 
}).AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>
    (
    mongoDbSettings.ConnectionString, mongoDbSettings.DatabaseName
    );



//builder.Services.AddDefaultIdentity<Mentat.LoginRegister.Areas.Identity.Data.ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<DbContext>();;

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
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
app.UseAuthentication();;

app.UseAuthorization();

app.UseEndpoints(endpoints => 
{ endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    );
    endpoints.MapRazorPages();
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
