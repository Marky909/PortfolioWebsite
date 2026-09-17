using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PortfolioWebsite.Data;
using PortfolioWebsite.Models;

var builder = WebApplication.CreateBuilder(args);

// MVC
builder.Services.AddControllersWithViews();

// EF Core
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

// ASP.NET Core Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    .AddDefaultUI();


builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/User/SignIn";
    options.AccessDeniedPath = "/User/AccessDenied";
});
var app = builder.Build();

// Create Admin role and Admin user
using (var scope = app.Services.CreateScope())
{
    await AdminSeeder.SeedAsync(scope.ServiceProvider);
}

// HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

// IMPORTANT: Authentication before Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();