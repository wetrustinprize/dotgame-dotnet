using DotGameLogic;
using DotGameMemoryServer.Exceptions;

namespace DotGameMemoryServer;

public class Instance
{
    public Guid Id { get; }
    public DateTime CreatedAt { get; }

    private readonly List<Player> _players;
    public Game? Game;

    public Instance()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;

        _players = new List<Player>();
    }

    /// <summary>
    /// Adds a new player to the players list and Game.
    /// It only adds if the game didn't start yet.
    /// </summary>
    /// <param name="data">The custom data of the player</param>
    /// <exception cref="InstanceGameAlreadyStarted">If tried to add a player with the game already started</exception>
    public Player AddPlayer(object data)
    {
        if (Game != null) throw new InstanceGameAlreadyStarted();

        var newPlayer = new Player
        {
            Data = data
        };

        _players.Add(newPlayer);

        return newPlayer;
    }

    /// <summary>
    /// Removes a player from the game.
    /// If the game has already started, will mark the player as left the game,
    /// otherwise will remove it completely.
    /// </summary>
    /// <param name="guid">The player to be removed guid</param>
    public void RemovePlayer(Guid guid)
    {
        if (Game == null)
            _players.RemoveAll(player => player.Id == guid);
        else
            Game.RemovePlayer(guid);
    }

    /// <summary>
    /// Starts the game.
    /// </summary>
    /// <param name="boardConfig">The board configuration.</param>
    /// <returns>The newly started game.</returns>
    /// <exception cref="InstanceGameAlreadyStarted">If the game has already been started.</exception>
    public Game StartGame(BoardConfig boardConfig)
    {
        if (Game != null) throw new InstanceGameAlreadyStarted();

        Game = new Game(_players, boardConfig);

        return Game;
    }
}