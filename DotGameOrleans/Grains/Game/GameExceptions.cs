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
public class GameNotInitializedException : GameException
{
    public GameNotInitializedException(Guid grain) : base(
        $"Game with grain id {grain.ToString()} is not initialized")
    {
    }
}

[Serializable]
[GenerateSerializer]
public class NotInGameException : GameException
{
    public NotInGameException(Guid grain, Guid session) : base(
        $"Session with id {session.ToString()} is not in game with grain id {grain.ToString()}")
    {
    }
}

[Serializable]
[GenerateSerializer]
public class OwnerNotInGameException : GameException
{
    public OwnerNotInGameException(Guid grain, Guid session) : base(
        $"Session is missing owner id ({session.ToString()}) from players in game with grain id {grain.ToString()}")
    {
    }
}

[Serializable]
[GenerateSerializer]
public class NotInTurnException : GameException
{
    public NotInTurnException(Guid grain, Guid session) : base(
        $"Session with id {session.ToString()} is not in turn in game with grain id {grain.ToString()}")
    {
    }
}