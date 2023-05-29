using DotGameOrleans_Test.Cluster;
using DotGameOrleans.Grains.Session;
using Orleans.TestingHost;

namespace DotGameOrleans_Test.SessionTests;

[Collection(ClusterCollection.Name)]
public class SessionTests
{
    private readonly TestCluster _cluster;

    public SessionTests(ClusterFixture fixture)
    {
        _cluster = fixture.Cluster;
    }

    [Fact]
    public async Task NewSession_ShouldNotBeInitialized()
    {
        var grain = _cluster.GrainFactory.GetGrain<ISessionGrain>(Guid.NewGuid());

        await Assert.ThrowsAsync<SessionNotInitialized>(async () =>
            await grain.GetState());
    }
}