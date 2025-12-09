using Microsoft.EntityFrameworkCore;
using Shop.Core.ServiceInterface;
using Shop.ApplicationServices.Services;
using Shop.Data;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Shop.Hubs;
using Microsoft.AspNetCore.Identity;
using Shop.Core.Domain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ISpaceshipServices, SpaceshipServices>();
builder.Services.AddScoped<IFileServices, FileServices>();
builder.Services.AddScoped<IRealEstateServices, RealEstateServices>();
builder.Services.AddScoped<IWeatherForecastServices, WeatherForecastServices>();
builder.Services.AddHttpClient<IChuckNorrisServices, ChuckNorrisServices>();
builder.Services.AddHttpClient<ICockTailsServices, CockTailsServices>();

builder.Services.AddScoped<EmailServices>();
builder.Services.AddScoped<IEmailServices, EmailServices>();

builder.Services.AddSignalR();

builder.Services.AddDbContext<ShopContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 3;
})

    .AddEntityFrameworkStores<ShopContext>()
    .AddDefaultTokenProviders()
    .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>("CustomEmailConfirmation");
/*.AddDefaultUI()*/

var app = builder.Build();

app.UseHttpsRedirection();


app.UseRouting();

app.UseAuthorization();

var provider = new FileExtensionContentTypeProvider();
app.UseStaticFiles();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


//signal R part

app.MapHub<ChatHub>("/chathub");

app.Run();

