using DotGameOrleans.Grains;
using DotGameOrleans.Grains.Interfaces;
using Orleans.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<ILobbyGrain, LobbyGrain>();
builder.Services.AddTransient<ISessionGrain, SessionGrain>();

builder.Host.UseOrleans(siloBuilder =>
{
    siloBuilder.UseLocalhostClustering();
    siloBuilder.AddMemoryGrainStorageAsDefault();
});

var app = builder.Build();
app.Run();