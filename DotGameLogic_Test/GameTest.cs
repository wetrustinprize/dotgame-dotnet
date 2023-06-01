using DotGameLogic;

namespace DotGameLogic_Test;

public class GameTest
{
    private readonly List<Player> _fakePlayers = Player.FromData(new[]
    {
        "wetrustinprize",
        "ruuby",
        "bode"
    });

    private readonly BoardConfig _defaultConfig = new BoardConfig
    {
        Height = 4,
        Width = 4
    };

    [Fact]
    public void Game_NextPlayerLoop()
    {
        var game = new Game(_fakePlayers, _defaultConfig);

        for (var i = 0; i < _fakePlayers.Count; i++)
        {
            Assert.Equal(i, game.CurrentPlayerIndex);
            Assert.Equal(_fakePlayers[i].Data, game.CurrentPlayer.Data);

            game.NextPlayer();
        }

        Assert.Equal(0, game.CurrentPlayerIndex);
        Assert.Equal(_fakePlayers[0].Data, game.CurrentPlayer.Data);
    }

    [Fact]
    public void Game_SkipPlayer()
    {
        var game = new Game(_fakePlayers, _defaultConfig);
        game.RemovePlayer(1);
        game.NextPlayer();

        Assert.Equal(2, game.CurrentPlayerIndex);
    }

    [Fact]
    public void Game_SkipPlayerOnTurn()
    {
        var game = new Game(_fakePlayers, _defaultConfig);
        game.RemovePlayer(0);

        Assert.Equal(1, game.CurrentPlayerIndex);
    }
}