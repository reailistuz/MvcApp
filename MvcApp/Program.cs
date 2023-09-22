using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcApp.Controllers;
using MvcApp.Data;
using MvcApp.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MvcAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MvcAppContext") ?? throw new InvalidOperationException("Connection string 'MvcAppContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<ICsvService, CsvService>();

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
