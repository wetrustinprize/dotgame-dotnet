using DotGameLogic;
using DotGameLogic.Exceptions;

namespace DotGameOrleans.Grains.Game;

[GenerateSerializer]
public class GameGrainState
{
    [Id(0)] public bool Initialized { get; init; }
    [Id(1)] public List<GamePlayer> Players { get; init; } = new();
    [Id(2)] public int CurrentPlayerIndex { get; set; } = -1;
    [Id(3)] public Board Board { get; init; } = null!;
    [Id(4)] public Guid Owner { get; init; } = Guid.Empty;
}

public interface IGameGrain : IGrainWithGuidKey
{
    /// <summary>
    /// Initializes the game
    /// </summary>
    /// <param name="players">The players of the game</param>
    /// <param name="owner">The owner of this game</param>
    /// <param name="height">The height of the board</param>
    /// <param name="width">The width of the board</param>
    /// <returns></returns>
    /// <exception cref="InvalidBoardSize">Inputted board size is invalid</exception>
    public Task Init(IEnumerable<Guid> players, Guid owner, int height, int width);

    /// <summary>
    /// Gets the current state of the game
    /// </summary>
    /// <returns>The current state of the game</returns>
    public Task<GameGrainState> GetState();

    /// <summary>
    /// Removes a session from the game
    /// </summary>
    /// <param name="session"></param>
    public Task RemoveSession(Guid session);

    /// <summary>
    /// Check if is the owner of the game
    /// </summary>
    /// <param name="session">The session to check the ownership</param>
    /// <returns>True if is the owner</returns>
    public Task<bool> IsOwner(Guid session);
}