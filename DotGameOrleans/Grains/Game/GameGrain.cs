using DotGameLogic;

namespace DotGameOrleans.Grains.Game;

public class GameGrain : Grain<GameGrainState>, IGameGrain
{
    public Task Init(IEnumerable<Guid> players, int height, int width)
    {
        State = new GameGrainState
        {
            Initialized = true,
            Players = players.Select(session => new GamePlayer
            {
                Session = session
            }).ToList(),
            CurrentPlayerIndex = 0,
            Board = new Board(height, width)
        };

        return Task.CompletedTask;
    }
}