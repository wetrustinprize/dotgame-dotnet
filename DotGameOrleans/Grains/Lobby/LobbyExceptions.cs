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
    public LobbyNotInitialized(Guid grainId) : base($"Session with grain id {grainId.ToString()} is not initialized")
    {
    }
}

[Serializable]
[GenerateSerializer]
public class LobbyInProgress : LobbyException
{
    public LobbyInProgress(Guid grainId) : base($"Session with grain id {grainId.ToString()} is already in process")
    {
    }
}

[Serializable]
[GenerateSerializer]
public class NotInLobby : LobbyException
{
    public NotInLobby(Guid grainId, Guid playerId) : base(
        $"Player with id {playerId.ToString()} is not in session with grain id {grainId.ToString()}")
    {
    }
}

[Serializable]
[GenerateSerializer]
public class LobbyAlreadyJoined : LobbyException
{
    public LobbyAlreadyJoined(Guid grainId, Guid playerId) : base(
        $"Player with id {playerId.ToString()} already joined session with grain id {grainId.ToString()}")
    {
    }
}