using DotGameOrleans.Grains;
using DotGameOrleans.Grains.Interfaces;
using DotGameOrleans.Grains.Lobby;
using DotGameOrleans.Grains.Session;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<ILobbyGrain, LobbyGrain>();
builder.Services.AddTransient<ISessionGrain, SessionGrain>();

builder.Host.UseOrleans(siloBuilder =>
{
    siloBuilder.UseLocalhostClustering();
    siloBuilder.AddMemoryGrainStorageAsDefault();
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var filePath = Path.Combine(AppContext.BaseDirectory, "RestfulAPI.xml");
    c.IncludeXmlComments(filePath);
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();