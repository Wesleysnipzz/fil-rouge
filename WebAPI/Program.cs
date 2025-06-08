using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models.Profiles;
using Shared.Data;
using Shared.forme; // Pour accéder à GameManager
using Shared.Interface; // Ajout de l'importation manquante
using Microsoft.Extensions.Logging; // Pour les logs

var builder = WebApplication.CreateBuilder(args);

// Configuration des services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPI", Version = "v1" });
});
// Configuration CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost3000",
        builder => builder.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

// Enregistrer AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Enregistrement de GameManager pour injection de dépendances
builder.Services.AddScoped<IGameManager, GameManager>();

// Ajouter la configuration des logs
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();
// Initialisation de la base de données avec un échiquier par défaut
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        
        // Vérifier si des boards existent déjà
        if (!context.Boards.Any())
        {
            // Créer au moins un échiquier par défaut
            context.Boards.Add(new Board
            {
                Id = 1,
                Name = "Échiquier principal",
                Type = "standard",
                CreatedAt = DateTime.Now
            });
            context.SaveChanges();
            
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogInformation("Base de données initialisée avec un échiquier par défaut");
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Une erreur s'est produite lors de l'initialisation de la base de données");
    }
}

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
app.UseCors("AllowLocalhost3000");

app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.UseHttpsRedirection();

// Permet à l'API d'écouter sur toutes les interfaces réseau
app.Run();  //("http://0.0.0.0:5106");

// Contrôleur de test pour vérifier que l'API fonctionne
app.MapGet("/api/test", () => new { message = "API is working!" });