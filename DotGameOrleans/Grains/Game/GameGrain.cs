namespace DotGameOrleans.Grains.Game;

public class GameGrain : Grain<GameGrainState>, IGameGrain
{
    public Task Init(HashSet<Guid> players, int height, int width)
    {
        throw new NotImplementedException();
    }
}