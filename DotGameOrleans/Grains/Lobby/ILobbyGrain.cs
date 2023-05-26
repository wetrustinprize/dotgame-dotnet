using DotGameLogic;

namespace DotGameOrleans.Grains.Lobby;

[GenerateSerializer]
public class LobbyGrainState
{
    [Id(5)] public bool Initialized { get; init; }
    
    [Id(0)] public Board Board { get; init; } = null!;
    [Id(4)] public LobbyStateEnum State { get; set; } = LobbyStateEnum.WaitingForPlayers;
    
    [Id(1)]
    public HashSet<Guid> Players { get; } = new();
    
    [Id(2)]
    public Guid Owner { get; init; } = Guid.Empty;

    [Id(3)] public Guid CurrentPlayer { get; set; } = Guid.Empty;
}

public interface ILobbyGrain : IGrainWithGuidKey
{
    /// <summary>
    /// Initializes a new lobby with the given owner and board size.
    /// </summary>
    /// <param name="owner">The owner of the lobby</param>
    /// <param name="boardHeight">The board height</param>
    /// <param name="boardWidth">The board width</param>
    public Task Init(Guid owner, int boardHeight, int boardWidth);
    
    /// <summary>
    /// Gets the lobby owner
    /// </summary>
    /// <returns>The owner's guid</returns>
    public Task<Guid> GetOwner();
    
    /// <summary>
    /// Gets the current state of a session
    /// </summary>
    /// <exception cref="LobbyNotInitialized">If this grain was not been initialized yet</exception>
    public Task<LobbyGrainState> GetState();
}