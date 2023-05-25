using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Orleans.Serialization;
using RestfulAPI.Filters;
using RestfulAPI.SwaggerConfig;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseOrleansClient(siloBuilder =>
{
    siloBuilder.UseLocalhostClustering();
    siloBuilder.Services.AddSerializer(serializerBuilder =>
    {
        serializerBuilder.AddJsonSerializer(isSupported: type =>
            type.Namespace != null && type.Namespace.StartsWith("DotGameLogic"));
    });
});

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers(options =>
{
    options.Filters.Add<SessionExceptionsFilter>();
    options.Filters.Add<LobbyExceptionsFilter>();
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
});
    
builder.Services.AddVersionedApiExplorer(options =>
{
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfigOptions>();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var versionDescProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    foreach (var desc in versionDescProvider.ApiVersionDescriptions)
    {
        options.SwaggerEndpoint($"/swagger/{desc.GroupName}/swagger.json", $"DotGame - {desc.ApiVersion.ToString()}");
    }
});
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();