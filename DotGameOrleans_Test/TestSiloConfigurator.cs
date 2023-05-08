using Orleans.TestingHost;

namespace DotGameOrleans_Test;

public class TestSiloConfigurator : ISiloConfigurator
{
    public void Configure(ISiloBuilder hostBuilder)
    {
        hostBuilder
            .UseLocalhostClustering()
            .AddMemoryGrainStorageAsDefault();
    }
}