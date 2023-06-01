namespace DotGameLogic;

public class Game
{
    #region Variables

    public List<Player> Players { get; }
    public int CurrentPlayerIndex { get; private set; }
    public Player CurrentPlayer => Players[CurrentPlayerIndex];
    public int PlayersCount => Players.Count(p => !p.LeftGame);
    public Board Board { get; private set; }

    #endregion

    public Game(List<Player> players, BoardConfig board)
    {
        Players = players;
        Board = new Board(board);

        CurrentPlayerIndex = 0;
    }

    /// <summary>
    /// Moves the current player index to the next player
    /// Skips all players that left the game
    /// </summary>
    /// <param name="index">The new index</param>
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
}