namespace DotGameOrleans.Grains.Game;

[GenerateSerializer]
public class GameGrainState
{
    [Id(0)] public bool Initialized { get; init; }
    [Id(1)] public HashSet<Guid> Players { get; } = new();
}

public interface IGameGrain : IGrainWithGuidKey
{
    public Task Init(HashSet<Guid> players, int height, int width);
}