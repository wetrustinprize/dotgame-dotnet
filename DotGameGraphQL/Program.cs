using DotGameGraphQL.Session;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddGraphQLServer()
    .AddQueryType(q => q.Name("Query"))
    .AddType<SessionQuery>()
    .AddMutationType(m => m.Name("Mutation"))
    .AddType<SessionMutation>();

builder.Host.UseOrleansClient(clientBuilder =>
{
    clientBuilder.UseLocalhostClustering();
});

var app = builder.Build();

app.MapGraphQL();
app.Run();