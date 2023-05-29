using Microsoft.Extensions.Configuration;
using Orleans.Serialization;
using Orleans.TestingHost;

namespace DotGameOrleans_Test.Cluster;

public class TestClientConfigurator : IClientBuilderConfigurator
{
    public void Configure(IConfiguration configuration, IClientBuilder clientBuilder)
    {
        clientBuilder.Services.AddSerializer(serializerBuilder =>
        {
            serializerBuilder.AddJsonSerializer(isSupported: type =>
                type.Namespace != null && type.Namespace.StartsWith("DotGameLogic"));
        });
    }
}