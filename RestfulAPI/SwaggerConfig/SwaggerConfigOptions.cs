using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

#pragma warning disable CS1591

namespace RestfulAPI.SwaggerConfig;

public class SwaggerConfigOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _apiVersionDescriptionProvider;

    public SwaggerConfigOptions(IApiVersionDescriptionProvider apiVersionDescriptionProvider)
    {
        _apiVersionDescriptionProvider = apiVersionDescriptionProvider;
    }

    public void Configure(SwaggerGenOptions options)
    {
        var xmlDocs = Path.Combine(AppContext.BaseDirectory, "RestfulAPI.xml");
        options.IncludeXmlComments(xmlDocs);
        
        foreach (var desc in _apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(desc.GroupName, new OpenApiInfo
            {
                Title = "DotGame restful interface",
                Version = desc.ApiVersion.ToString()
            });
        }
    }
}