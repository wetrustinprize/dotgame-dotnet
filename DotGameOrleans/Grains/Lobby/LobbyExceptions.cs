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
    public LobbyNotInitializedException(Guid grain) : base(
        $"Session with grain id {grain.ToString()} is not initialized")
    {
    }
}

[Serializable]
[GenerateSerializer]
public class NotInLobbyException : LobbyException
{
    public NotInLobbyException(Guid grain, Guid player) : base(
        $"Player with id {player.ToString()} is not in session with grain id {grain.ToString()}")
    {
    }
}

[Serializable]
[GenerateSerializer]
public class LobbyAlreadyJoinedException : LobbyException
{
    public LobbyAlreadyJoinedException(Guid grain, Guid player) : base(
        $"Player with id {player.ToString()} already joined session with grain id {grain.ToString()}")
    {
    }
}

[Serializable]
[GenerateSerializer]
public class NotEnoughPlayersException : LobbyException
{
    public NotEnoughPlayersException(Guid grain) : base(
        $"Session with grain id {grain.ToString()} does not have enough players")
    {
    }
}