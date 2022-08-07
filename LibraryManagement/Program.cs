

using DataLibrary;
using LibraryServices;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Adding DbContext.
builder.Services.AddDbContext<LibraryContext>(options 
    => options.UseSqlServer(builder.Configuration.GetConnectionString("LibraryConnection")));

//Remove 2 lines below
var config = builder.Configuration;
builder.Services.AddSingleton(config);

builder.Services.AddScoped<ILibraryAsset,LibraryAssetService>();
builder.Services.AddScoped<ICheckOut, CheckOutService>();


//builder.Services.AddApplicationInsightsTelemetry();

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
