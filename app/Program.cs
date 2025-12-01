using System.Text.Json.Serialization;
using RadarRendaFixa.Api.Contracts;
using RadarRendaFixa.Api.Repositories;
using RadarRendaFixa.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// DI
builder.Services.AddSingleton<ITabelaIrService, TabelaIrService>();
builder.Services.AddSingleton<ISimuladorRendaFixaService, SimuladorRendaFixaService>();
builder.Services.AddSingleton<IOfertaRepository, InMemoryOfertaRepository>();
builder.Services.AddSingleton<IRankingService, RankingService>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});


var app = builder.Build();

app.UseCors();


app.MapGet("/health", () => Results.Ok(new { status = "ok" }));

app.MapPost("/ranking-renda-fixa", (RankingRequest request, IRankingService rankingService) =>
{
    var resultado = rankingService.ObterRanking(request);
    return Results.Ok(resultado);
});

app.Run();
