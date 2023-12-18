using GameZone.Data;
using GameZone.Services;
using GameZone.Services.Category_Repository;
using GameZone.Services.Device_Repository;
using GameZone.Services.Game_Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


var connectionstring = builder.Configuration.GetConnectionString("Default")
    ?? throw new InvalidOperationException("No Connection String in Your App ");
builder.Services.AddDbContext<ApplicationDbContext>(options=>options.UseSqlServer(connectionstring));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ICategories, Categories>();
builder.Services.AddScoped<IDevices,Devices>();
builder.Services.AddScoped<IGameService,GameService>();

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
