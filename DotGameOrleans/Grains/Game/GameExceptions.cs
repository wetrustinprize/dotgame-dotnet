namespace DotGameOrleans.Grains.Game;

[Serializable]
[GenerateSerializer]
public class GameException : Exception
{
    public GameException()
    {
    }

    public GameException(string message) : base(message)
    {
    }

    public GameException(string message, Exception inner) : base(message, inner)
    {
    }
}

[Serializable]
[GenerateSerializer]
public class InvalidGameBoardSize : GameException
{
    public InvalidGameBoardSize(
        int inputtedWidth,
        int inputtedHeight,
        int minimum
    ) : base($"Can't make a board with {inputtedWidth} by {inputtedHeight}, minimum is {minimum} by {minimum}")
    {
    }
}