using DotGameMemoryServer;
using DotGameRestful.Authorization;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<MemoryServer>();

builder.Services
    .AddAuthentication(UserAuthSchemeOptions.DefaultScheme)
    .AddScheme<UserAuthSchemeOptions, UserAuthHandler>(
        UserAuthSchemeOptions.DefaultScheme,
        _ => { }
    );

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.OperationFilter<AuthOperationFilter>();

    var filePath = Path.Combine(AppContext.BaseDirectory, "RestfulAPI.xml");
    c.IncludeXmlComments(filePath);

    c.AddSecurityDefinition(UserAuthSchemeOptions.DefaultScheme, new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter the Player guid",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "GUID",
        Scheme = "Bearer"
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();