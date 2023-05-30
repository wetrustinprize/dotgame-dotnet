using DotGameOrleans_Test.Cluster;
using DotGameOrleans.Grains.Lobby;
using Orleans.TestingHost;

namespace DotGameOrleans_Test.LobbyTests;

[Collection(ClusterCollection.Name)]
public class LobbyTests
{
    private readonly TestCluster _cluster;

    public LobbyTests(ClusterFixture fixture)
    {
        _cluster = fixture.Cluster;
    }

    [Fact]
    public async Task NewLobby_OwnerShouldBeSet()
    {
        var ownerGuid = Guid.NewGuid();
        var lobbyGrain = _cluster.GrainFactory.GetGrain<ILobbyGrain>(Guid.NewGuid());

        await lobbyGrain.Init(ownerGuid);

        Assert.Equal(ownerGuid, await lobbyGrain.GetOwner());
    }

    [Fact]
    public async Task NewLobby_ShouldNotBeInitialized()
    {
        var lobbyGrain = _cluster.GrainFactory.GetGrain<ILobbyGrain>(Guid.NewGuid());

        await Assert.ThrowsAsync<LobbyNotInitialized>(async () =>
            await lobbyGrain.GetState()
        );
    }

    [Fact]
    public async Task JoinLobby_AlreadyJoined()
    {
        var sessionGuid = Guid.NewGuid();
        var lobbyGrain = _cluster.GrainFactory.GetGrain<ILobbyGrain>(Guid.NewGuid());

        await lobbyGrain.Init(sessionGuid);

        await Assert.ThrowsAsync<LobbyAlreadyJoined>(async () =>
            await lobbyGrain.AddPlayer(sessionGuid));
    }

    [Fact]
    public async Task StartGame_NotEnoughPlayers()
    {
        var lobbyGrain = _cluster.GrainFactory.GetGrain<ILobbyGrain>(Guid.NewGuid());

        await lobbyGrain.Init(Guid.NewGuid());

        await Assert.ThrowsAsync<NotEnoughPlayers>(async () =>
            await lobbyGrain.StartGame());
    }

    [Fact]
    public async Task StartGame_AlreadyStarted()
    {
        var lobbyGrain = _cluster.GrainFactory.GetGrain<ILobbyGrain>(Guid.NewGuid());

        await lobbyGrain.Init(Guid.NewGuid());
        await lobbyGrain.AddPlayer(Guid.NewGuid());
        await lobbyGrain.StartGame();

        await Assert.ThrowsAsync<LobbyInProgress>(async () =>
            await lobbyGrain.StartGame());
    }
}