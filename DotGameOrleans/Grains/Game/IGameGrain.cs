namespace DotGameOrleans.Grains.Game;

[GenerateSerializer]
public class GameGrainState
{
    [Id(0)] public bool Initialized { get; init; }
    [Id(1)] public HashSet<Guid> Players { get; } = new();
}

public interface IGameGrain : IGrainWithGuidKey
{
    /// <summary>
    /// Initializes the game
    /// </summary>
    /// <param name="players">The players of the game</param>
    /// <param name="height">The height of the board</param>
    /// <param name="width">The width of the board</param>
    /// <returns></returns>
    /// <exception cref="InvalidGameBoardSize">Inputted board size is invalid</exception>
    public Task Init(IEnumerable<Guid> players, int height, int width);
}