using DotGameOrleans.Grains.Lobby;
using Orleans.TestingHost;

namespace DotGameOrleans_Test.LobbyTests;

[TestClass]
public class LobbyTests
{
    private TestCluster _cluster;
    
    public LobbyTests()
    {
        var builder = new TestClusterBuilder()
            .AddSiloBuilderConfigurator<TestSiloConfigurator>();
        
        _cluster = builder.Build();
        _cluster.Deploy();
    }
    
    [TestMethod]
    public async Task NewLobby_OwnerShouldBeSet()
    {
        var ownerGuid = Guid.NewGuid();
        var lobbyGrain = _cluster.GrainFactory.GetGrain<ILobbyGrain>(Guid.NewGuid());

        await lobbyGrain.Init(ownerGuid, 4, 4);
        
        Assert.AreEqual(ownerGuid, await lobbyGrain.GetOwner());
    }
}