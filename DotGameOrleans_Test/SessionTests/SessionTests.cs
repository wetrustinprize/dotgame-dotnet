using DotGameOrleans.Grains.Interfaces;
using Orleans.TestingHost;

namespace DotGameOrleans_Test.SessionTests;

[TestClass]
public class SessionTests
{
    private TestCluster _cluster;
    
    public SessionTests()
    {
        var builder = new TestClusterBuilder();
        
        _cluster = builder.Build();
        _cluster.Deploy();
    }
    
    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task NewSession_ShouldNotBeInitialized()
    {
        var grain = _cluster.GrainFactory.GetGrain<ISessionGrain>(Guid.NewGuid());
        
        await grain.GetState();
    }
}