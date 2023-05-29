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

        await lobbyGrain.Init(ownerGuid, 4, 4);

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

        await lobbyGrain.Init(sessionGuid, 4, 4);

        await Assert.ThrowsAsync<LobbyAlreadyJoined>(async () =>
            await lobbyGrain.AddPlayer(sessionGuid));
    }

    [Fact]
    public async Task JoinLobby_LobbyStarted()
    {
        var lobbyGrain = _cluster.GrainFactory.GetGrain<ILobbyGrain>(Guid.NewGuid());

        await lobbyGrain.Init(Guid.NewGuid(), 4, 4);
        await lobbyGrain.AddPlayer(Guid.NewGuid());
        await lobbyGrain.StartGame();

        await Assert.ThrowsAsync<LobbyInProgress>(async () =>
            await lobbyGrain.AddPlayer(Guid.NewGuid()));
    }

    [Fact]
    public async Task StartGame_NotEnoughPlayers()
    {
        var lobbyGrain = _cluster.GrainFactory.GetGrain<ILobbyGrain>(Guid.NewGuid());

        await lobbyGrain.Init(Guid.NewGuid(), 4, 4);

        await Assert.ThrowsAsync<NotEnoughPlayers>(async () =>
            await lobbyGrain.StartGame());
    }

    [Fact]
    public async Task StartGame_AlreadyStarted()
    {
        var lobbyGrain = _cluster.GrainFactory.GetGrain<ILobbyGrain>(Guid.NewGuid());

        await lobbyGrain.Init(Guid.NewGuid(), 4, 4);
        await lobbyGrain.AddPlayer(Guid.NewGuid());
        await lobbyGrain.StartGame();

        await Assert.ThrowsAsync<LobbyInProgress>(async () =>
            await lobbyGrain.StartGame());
    }

    [Fact]
    public async Task LeaveLobby_NotJoined()
    {
        var sessionGuid = Guid.NewGuid();
        var lobbyGrain = _cluster.GrainFactory.GetGrain<ILobbyGrain>(Guid.NewGuid());

        await lobbyGrain.Init(Guid.NewGuid(), 4, 4);

        await Assert.ThrowsAsync<NotInLobby>(async () =>
            await lobbyGrain.RemovePlayer(sessionGuid));
    }
}