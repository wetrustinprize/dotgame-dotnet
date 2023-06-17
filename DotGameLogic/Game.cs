namespace DotGameLogic;

public class Game
{
    public List<Player> Players { get; }
    public int CurrentPlayerIndex { get; private set; }
    public Player CurrentPlayer => Players[CurrentPlayerIndex];
    public int PlayersCount => Players.Count(p => !p.LeftGame);
    public Board Board { get; private set; }


    public Game(List<Player> players, BoardConfig board)
    {
        Players = players;
        Board = new Board(board);

        CurrentPlayerIndex = PlayersCount > 0 ? 0 : -1;
    }

    /// <summary>
    /// Moves the current player index to the next player
    /// Skips all players that left the game
    /// </summary>
    public int NextPlayer()
    {
        if (PlayersCount <= 1) return CurrentPlayerIndex;

        void SkipPlayer()
        {
            CurrentPlayerIndex++;
            if (CurrentPlayerIndex + 1 > Players.Count)
                CurrentPlayerIndex = 0;
        }

        SkipPlayer();
        while (CurrentPlayer.LeftGame)
            SkipPlayer();

        return CurrentPlayerIndex;
    }

    /// <summary>
    /// Marks a player as left the game
    /// </summary>
    /// <param name="index">The player index</param>
    public void RemovePlayer(int index)
    {
        var player = Players[index];
        player.LeftGame = true;

        Players[index] = player;

        if (CurrentPlayerIndex == index)
            NextPlayer();
    }

    /// <summary>
    /// Marks a player as left the game
    /// </summary>
    /// <param name="guid">The player guid</param>
    public void RemovePlayer(Guid guid)
    {
        var playerIndex = Players.FindIndex(player => player.Id == guid);

        if (playerIndex == -1) return;
        RemovePlayer(playerIndex);
    }
}