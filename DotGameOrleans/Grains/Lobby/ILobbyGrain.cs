namespace DotGameOrleans.Grains.Lobby;

[GenerateSerializer]
public class LobbyGrainState
{
    [Id(5)] public bool Initialized { get; init; }
    [Id(1)] public HashSet<Guid> Players { get; } = new();
    [Id(2)] public Guid Owner { get; init; } = Guid.Empty;
}

public interface ILobbyGrain : IGrainWithGuidKey
{
    /// <summary>
    /// Initializes a new lobby with the given owner and board size.
    /// </summary>
    /// <param name="owner">The owner of the lobby</param>
    public Task Init(Guid owner);

    /// <summary>
    /// Gets the lobby owner session
    /// </summary>
    /// <returns>The owner's session guid</returns>
    public Task<Guid> GetOwner();

    /// <summary>
    /// Checks if the given session is the owner of this lobby
    /// </summary>
    /// <param name="session">The session GUID</param>
    /// <returns></returns>
    public Task<bool> IsOwner(Guid session);

    /// <summary>
    /// Gets the current state of a session
    /// </summary>
    /// <exception cref="LobbyNotInitialized">If this grain was not been initialized yet</exception>
    public Task<LobbyGrainState> GetState();

    /// <summary>
    /// Starts the game and empty the lobby
    /// </summary>
    /// <param name="height">The board height</param>
    /// <param name="width">The board width</param>
    /// <exception cref="NotEnoughPlayers">There are just a single player in the lobby</exception>
    public Task StartGame(int height, int width);

    /// <summary>
    /// Adds a session to the lobby
    /// </summary>
    /// <param name="session">The session GUID to be added</param>
    /// <exception cref="LobbyAlreadyJoined">If the <paramref name="session"/> has already been added to this lobby</exception>
    public Task AddPlayer(Guid session);

    /// <summary>
    /// Removes a session from the lobby
    /// </summary>
    /// <param name="session">The session GUID</param>
    /// <exception cref="NotInLobby">If the <paramref name="session"/> wasn't in the lobby</exception>
    public Task RemovePlayer(Guid session);
}