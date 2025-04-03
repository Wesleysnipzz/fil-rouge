using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models.Profiles;
using Shared.Data;
using Shared.forme; // Pour accéder à GameManager
using Microsoft.Extensions.Logging; // Pour les logs

var builder = WebApplication.CreateBuilder(args);

// Configuration des services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPI", Version = "v1" });
});

// Enregistrer AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Enregistrement de GameManager pour injection de dépendances
builder.Services.AddScoped<GameManager>();

// Ajouter la configuration des logs
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

// Configuration du pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1");
        c.RoutePrefix = string.Empty;
    });
}

// Désactiver la redirection HTTPS si pas nécessaire en production
if (!app.Environment.IsDevelopment())
{
}

app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.UseHttpsRedirection();

// Permet à l'API d'écouter sur toutes les interfaces réseau
app.Run("http://0.0.0.0:5106");

// Contrôleur de test pour vérifier que l'API fonctionne
app.MapGet("/api/test", () => new { message = "API is working!" });