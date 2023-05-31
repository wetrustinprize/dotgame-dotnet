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
public class LobbyNotInitializedException : LobbyException
{
    public LobbyNotInitializedException(Guid grainId) : base($"Session with grain id {grainId.ToString()} is not initialized")
    {
    }
}

[Serializable]
[GenerateSerializer]
public class NotInLobbyException : LobbyException
{
    public NotInLobbyException(Guid grainId, Guid playerId) : base(
        $"Player with id {playerId.ToString()} is not in session with grain id {grainId.ToString()}")
    {
    }
}

[Serializable]
[GenerateSerializer]
public class LobbyAlreadyJoinedException : LobbyException
{
    public LobbyAlreadyJoinedException(Guid grainId, Guid playerId) : base(
        $"Player with id {playerId.ToString()} already joined session with grain id {grainId.ToString()}")
    {
    }
}

[Serializable]
[GenerateSerializer]
public class NotEnoughPlayersException : LobbyException
{
    public NotEnoughPlayersException(Guid grainId) : base($"Session with grain id {grainId.ToString()} does not have enough players")
    {
    }
}