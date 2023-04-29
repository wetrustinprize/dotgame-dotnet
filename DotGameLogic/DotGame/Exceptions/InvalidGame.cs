namespace DotGameLogic.DotGame.Exceptions;

[Serializable]
public class InvalidGame : Exception
{
    public InvalidGame() {}
    public InvalidGame(string message) : base(message) {}
    public InvalidGame(string message, Exception inner) : base(message, inner) {}
}

[Serializable]
public class NotEnoughPlayers : InvalidGame
{
    public NotEnoughPlayers() {}
    public NotEnoughPlayers(string message) : base(message) {}
    public NotEnoughPlayers(string message, Exception inner) : base(message, inner) {}
}