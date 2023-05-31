using DotGameOrleans_Test.Cluster;
using DotGameOrleans.Grains.Game;
using Orleans.TestingHost;

namespace DotGameOrleans_Test.GameTests;

[Collection(ClusterCollection.Name)]
public class GameTests
{
    private readonly TestCluster _cluster;

    public GameTests(ClusterFixture fixture)
    {
        _cluster = fixture.Cluster;
    }

    #region New Game

    [Fact]
    public async Task NewGame_ShouldNotBeInitialized()
    {
        var gameGrain = _cluster.GrainFactory.GetGrain<IGameGrain>(Guid.NewGuid());

        await Assert.ThrowsAsync<GameNotInitializedException>(async () =>
            await gameGrain.GetState()
        );
    }

    [Fact]
    public async Task NewGame_ShouldHaveOwnerInPlayers()
    {
        var gameGrain = _cluster.GrainFactory.GetGrain<IGameGrain>(Guid.NewGuid());

        var owner = Guid.NewGuid();
        await Assert.ThrowsAsync<OwnerNotInGameException>(async () =>
            await gameGrain.Init(new[] { Guid.NewGuid() }, owner, 4, 4)
        );
    }

    #endregion

    [Fact]
    public async Task LeftGame_RaiseErrorIfNotInIt()
    {
        var gameGrain = _cluster.GrainFactory.GetGrain<IGameGrain>(Guid.NewGuid());

        var owner = Guid.NewGuid();
        var player = Guid.NewGuid();

        await gameGrain.Init(new[] { owner, Guid.NewGuid(), player }, owner, 4, 4);
        await gameGrain.RemoveSession(player);

        await Assert.ThrowsAsync<NotInGameException>(async () => await gameGrain.RemoveSession(player));
    }
}