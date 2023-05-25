namespace DotGameOrleans.Grains.Lobby;

[Serializable]
[GenerateSerializer]
public class LobbyException : Exception
{
    public LobbyException()
    {
    }

    public LobbyException(string message) : base(message)
    {
    }

    public LobbyException(string message, Exception inner) : base(message, inner)
    {
    }
}

[Serializable]
[GenerateSerializer]
public class LobbyNotInitialized : LobbyException
{
    public LobbyNotInitialized(string grainId) : base($"Session with grain id {grainId} is not initialized")
    {
    }
}