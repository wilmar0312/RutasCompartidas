using System;
using Microsoft.EntityFrameworkCore;
using RutasCompartidas.Infrastructure.Persistence;
using RutasCompartidas.Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar el servicio AuthService
builder.Services.AddScoped<RutasCompartidas.Application.Services.AuthService>();

// Registrar el servicio RutaService
builder.Services.AddScoped<RutasCompartidas.Application.Services.RutaService>();


builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Ajusta el tiempo de inactividad
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


builder.Services.AddHttpClient("ApiRutas", client =>
{
    client.BaseAddress = new Uri("http://localhost:5018/api/");
});

builder.Services.AddScoped<RutasCompartidas.Web.Services.RutaApiService>();
builder.Services.AddScoped<AuthApiService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}");

app.Run();
