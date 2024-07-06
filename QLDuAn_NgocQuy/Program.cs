using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using QLDuAn_NgocQuy.Data;
using System.Data;
using System.Data.SqlClient;
var builder = WebApplication.CreateBuilder(args);
// setup dapper
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string 'Default' is missing or empty.");

}
//builder.Services.AddScoped<IConnectDapper>(sp => new ConnectDapper(connectionString));

// Ðãng k? d?ch v? IDbConnection
builder.Services.AddTransient<IDbConnection>(sp => new SqlConnection(connectionString));
//quan
// Load configuration
// builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register DuAnService with configuration parameter
builder.Services.AddScoped<DuAnService>(provider =>
    new DuAnService(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<PhanCongCVService>(provider =>
    new PhanCongCVService(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ThanhVienService>(provider =>
    new ThanhVienService(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
