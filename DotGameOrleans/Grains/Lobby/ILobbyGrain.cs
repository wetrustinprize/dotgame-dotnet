using DotGameLogic;

namespace DotGameOrleans.Grains.Interfaces;

[GenerateSerializer]
public class LobbyGrainState
{
    [Id(0)]
    public Board Board { get; set; }
}

public interface ILobbyGrain : IGrainWithStringKey
{
    
}