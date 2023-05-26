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
    
    [TestMethod]
    [ExpectedException(typeof(LobbyNotInitialized))]
    public async Task NewLobby_ShouldNotBeInitialized()
    {
        var lobbyGrain = _cluster.GrainFactory.GetGrain<ILobbyGrain>(Guid.NewGuid());
        
        await lobbyGrain.GetState();
    }

    [TestMethod]
    [ExpectedException(typeof(LobbyAlreadyJoined))]
    public async Task JoinLobby_AlreadyJoined()
    {
        var sessionGuid = Guid.NewGuid();
        var lobbyGrain = _cluster.GrainFactory.GetGrain<ILobbyGrain>(Guid.NewGuid());
        
        await lobbyGrain.Init(Guid.NewGuid(), 4, 4);
        lobbyGrain.AddPlayer(sessionGuid);
        lobbyGrain.AddPlayer(sessionGuid);
    }

    [TestMethod]
    [ExpectedException(typeof(LobbyInProgress))]
    public async Task JoinLobby_LobbyStarted()
    {
        throw new NotImplementedException();
    }

    [TestMethod]
    [ExpectedException(typeof(NotInLobby))]
    public async Task LeaveLobby_NotJoined()
    {
        var sessionGuid = Guid.NewGuid();
        var lobbyGrain = _cluster.GrainFactory.GetGrain<ILobbyGrain>(Guid.NewGuid());
        
        await lobbyGrain.Init(Guid.NewGuid(), 4, 4);
        lobbyGrain.RemovePlayer(sessionGuid);
    }
}