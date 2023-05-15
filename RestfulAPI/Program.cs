using System.Diagnostics;
using System.Reflection;
using DotGameOrleans.Grains.Interfaces;
using DotGameOrleans.Grains.Lobby;
using DotGameOrleans.Grains.Session;
using Microsoft.OpenApi.Models;

var assembly = Assembly.GetExecutingAssembly();
var fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
var version = fileVersionInfo.ProductVersion;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<ILobbyGrain, LobbyGrain>();
builder.Services.AddTransient<ISessionGrain, SessionGrain>();

builder.Host.UseOrleans(siloBuilder =>
{
    siloBuilder.UseLocalhostClustering();
    siloBuilder.AddMemoryGrainStorageAsDefault();
});

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var filePath = Path.Combine(AppContext.BaseDirectory, "RestfulAPI.xml");
    c.IncludeXmlComments(filePath);
    c.SwaggerDoc(version, new OpenApiInfo
    {
        Title = "DotGame Restful",
        Description = "DotGame restful interface.",
        Version = version
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint($"/swagger/{version}/swagger.json", version);
    options.RoutePrefix = string.Empty;
});
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();