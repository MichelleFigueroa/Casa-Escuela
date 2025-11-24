using CasaEscuela.AppWebMVC.Hubs;
using CasaEscuela.AppWebMVC.Models;
using CasaEscuela.AppWebMVC.Models.Configuracion;
using CasaEscuela.AppWebMVC.Utils;
using CasaEscuela.BL.Interfaces;
using CasaEscuela.DAL;
using CasaEscuela.DAL.Seed;
using CasaEscuela.IoC;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://0.0.0.0:5045");

// Servicios
builder.Services.AddServerSideBlazor()
    .AddCircuitOptions(options =>
    {
        options.DetailedErrors = builder.Configuration.GetValue<bool>("DetailedErrors");
    });


builder.Services.AddControllersWithViews();
builder.Services.AddIoCDependecies(builder.Configuration);
builder.Services.AddScoped<Credencial>();
builder.Services.AddProgressiveWebApp();
builder.Services.AddMemoryCache();
builder.Services.AddScoped<CacheService>();

builder.Services.AddScoped<ICodigoQrService, CodigoQrService>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<CasaEscuelaDBContext>((serviceProvider, options) =>
{
    var connectionString = builder.Configuration.GetConnectionString("Conn");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    options.EnableSensitiveDataLogging();
    options.EnableDetailedErrors();
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(o =>
{
    o.LoginPath = new PathString("/Usuario/login");
    o.AccessDeniedPath = new PathString("/Usuario/login");
    o.ExpireTimeSpan = TimeSpan.FromHours(8);
    o.SlidingExpiration = true;
    o.Cookie.HttpOnly = true;
});
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddSingleton<IUserIdProvider, NombreUsuarioIdProvider>();
builder.Services.Configure<ConfiguracionSistema>(
    builder.Configuration.GetSection("ConfiguracionSistema"));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<CasaEscuelaDBContext>();
    InicializarDatosSeed.Inicializar(dbContext);
}


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHub<NotificacionesHub>("/notificacionesHub");


app.MapBlazorHub();
app.MapFallbackToFile("index.html");

IWebHostEnvironment env = app.Environment;
Rotativa.AspNetCore.RotativaConfiguration.Setup(env.WebRootPath, "../wwwroot/Rotativa");

app.Run();
