using TesteTecnicoCrud.Api.Endpoints;
using TesteTecnicoCrud.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS (liberar geral em dev; ajuste depois)
builder.Services.AddCors(o =>
{
    o.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

// DI
builder.Services.AddSingleton<IClienteRepository, InMemoryClienteRepository>();

var app = builder.Build();

app.UseCors("AllowAll");

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => Results.Redirect("/swagger"));

// Endpoints por área
app.MapClientesEndpoints();

app.Run();
