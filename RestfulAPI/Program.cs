var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOrleansClient(clientBuilder =>
{
    clientBuilder.UseLocalhostClustering();
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