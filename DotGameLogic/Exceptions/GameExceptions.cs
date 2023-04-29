namespace DotGameLogic.Exceptions;

[Serializable]
public class GameException : Exception
{
    public GameException() {}
    public GameException(string message) : base(message) {}
    public GameException(string message, Exception inner) : base(message, inner) {}
}

[Serializable]
public class NotEnoughPlayers : GameException
{
    public NotEnoughPlayers() {}
    public NotEnoughPlayers(string message) : base(message) {}
    public NotEnoughPlayers(string message, Exception inner) : base(message, inner) {}
}