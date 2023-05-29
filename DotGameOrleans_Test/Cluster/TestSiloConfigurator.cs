using Orleans.Serialization;
using Orleans.TestingHost;

namespace DotGameOrleans_Test.Cluster;

public class TestSiloConfigurator : ISiloConfigurator
{
    public void Configure(ISiloBuilder siloBuilder)
    {
        siloBuilder.UseLocalhostClustering();
        siloBuilder.AddMemoryGrainStorageAsDefault();

        siloBuilder.Services.AddSerializer(serializerBuilder =>
        {
            serializerBuilder.AddJsonSerializer(isSupported: type =>
                type.Namespace != null && type.Namespace.StartsWith("DotGameLogic"));
        });
    }
}