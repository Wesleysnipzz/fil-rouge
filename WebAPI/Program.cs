using AutoMapper;
using WebAPI.Models.Profiles;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 🔥 Enregistrer AutoMapper 🔥
builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app
var builder = WebApplication.CreateBuilder(args);

// Ajoute cette ligne pour enregistrer AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddControllers();
var app = builder.Build();

app.UseRouting();
app.MapControllers();

app.Run();