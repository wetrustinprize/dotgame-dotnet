using DotGameOrleans.Grains.Game;
using DotGameOrleans.Grains.Lobby;
using DotGameOrleans.Grains.Session;
using Orleans.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<ILobbyGrain, LobbyGrain>();
builder.Services.AddTransient<ISessionGrain, SessionGrain>();
builder.Services.AddTransient<IGameGrain, GameGrain>();

builder.Host.UseOrleans(siloBuilder =>
{
    siloBuilder.UseLocalhostClustering();
    siloBuilder.AddMemoryGrainStorageAsDefault();

    siloBuilder.Services.AddSerializer(serializerBuilder =>
    {
        serializerBuilder.AddJsonSerializer(isSupported: type =>
            type.Namespace != null && type.Namespace.StartsWith("DotGameLogic"));
    });
});

var app = builder.Build();
app.Run();