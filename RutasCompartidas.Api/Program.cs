using Microsoft.EntityFrameworkCore;
using RutasCompartidas.Application.Services;
using RutasCompartidas.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Configurar conexión a SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar servicios de aplicación
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<RutaService>();

// Habilitar CORS (para permitir que otros clientes consuman la API)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// Agregar controladores a la API
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Usar CORS
app.UseCors("AllowAll");

// Habilitar Swagger
app.UseSwagger();
app.UseSwaggerUI();

// Configurar el pipeline de la API
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
