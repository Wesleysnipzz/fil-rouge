using Microsoft.OpenApi.Models;
using WebAPI.Models.Profiles;
using WebAPI.Data; // Ajoutez le namespace correct pour ApplicationDbContext

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

// Ajouter le contexte de base de données
builder.Services.AddDbContext<ApplicationDbContext>();

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

// Redirection HTTP et routage
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();
app.MapControllers();
app.Run();
