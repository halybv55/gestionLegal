using GestionLegalP.Infrastructure.Repositories;
using GestionLegalP.Application.Interfaces;
using GestionLegalP.Application.Services;
using GestionLegalP.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


//Conexión a Railway
// Conexión Railway / Local
var rawUrl = Environment.GetEnvironmentVariable("DATABASE_URL")
             ?? builder.Configuration.GetConnectionString("DefaultConnection");

string ConvertDatabaseUrl(string databaseUrl)
{
    if (string.IsNullOrEmpty(databaseUrl))
        throw new Exception("No se encontró la cadena de conexión.");

    if (databaseUrl.StartsWith("postgres://") || databaseUrl.StartsWith("postgresql://"))
    {
        var uri = new Uri(databaseUrl);
        var userInfo = uri.UserInfo.Split(':');

        var username = Uri.UnescapeDataString(userInfo[0]);
        var password = Uri.UnescapeDataString(userInfo[1]);
        var database = uri.AbsolutePath.TrimStart('/');

        return $"Host={uri.Host};Port={uri.Port};Database={database};Username={username};Password={password};SSL Mode=Require;Trust Server Certificate=true";
    }

    return databaseUrl;
}

var connectionString = ConvertDatabaseUrl(rawUrl);

Console.WriteLine($"Cadena convertida: {connectionString}");

builder.Services.AddDbContext<GestionLegalPContext>(options =>
    options.UseNpgsql(connectionString)
);

//Puerto para Railway
builder.WebHost.UseUrls("http://0.0.0.0:8080");


//Controllers y Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//CORS (para frontend o pruebas)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});


//Registrar repositorios y servicios (MUY IMPORTANTE)
builder.Services.AddScoped<ICasoLegalRepository, CasoLegalRepository>();
builder.Services.AddScoped<ICasoLegalService, CasoLegalService>();

builder.Services.AddScoped<IDocumentoLegalRepository, DocumentoLegalRepository>();
builder.Services.AddScoped<IDocumentoLegalService, DocumentoLegalService>();

builder.Services.AddScoped<IReglaRepository, ReglaRepository>();
builder.Services.AddScoped<IReglaService, ReglaService>();

builder.Services.AddScoped<ISolicitudRepository, SolicitudRepository>();
builder.Services.AddScoped<ISolicitudService, SolicitudService>();

builder.Services.AddScoped<IConsentimientoRepository, ConsentimientoRepository>();
builder.Services.AddScoped<IConsentimientoService, ConsentimientoService>();

builder.Services.AddScoped<ICasoDocumentoRepository, CasoDocumentoRepository>();
builder.Services.AddScoped<ICasoDocumentoService, CasoDocumentoService>();

builder.Services.AddScoped<ICasoInvolucradoRepository, CasoInvolucradoRepository>();
builder.Services.AddScoped<ICasoInvolucradoService, CasoInvolucradoService>();

builder.Services.AddScoped<IDocumentoDecisionRepository, DocumentoDecisionRepository>();
builder.Services.AddScoped<IDocumentoDecisionService, DocumentoDecisionService>();

builder.Services.AddScoped<IConsentimientoDocumentoRepository, ConsentimientoDocumentoRepository>();
builder.Services.AddScoped<IConsentimientoDocumentoService, ConsentimientoDocumentoService>();

builder.Services.AddScoped<ISolicitudRevisionRepository, SolicitudRevisionRepository>();
builder.Services.AddScoped<ISolicitudRevisionService, SolicitudRevisionService>();


var app = builder.Build();


//Migraciones automáticas
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<GestionLegalPContext>();
    dbContext.Database.Migrate();
}

builder.Services.AddHttpClient();

//Middleware
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();