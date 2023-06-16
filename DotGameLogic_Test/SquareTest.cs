using DotGameLogic;
using DotGameLogic.Enums;
using DotGameLogic.Exceptions;

namespace DotGameLogic_Test;

public class SquareTest
{
    [Fact]
    public void ConnectedSquares_ShareLines()
    {
        var top = new Square();
        var bottom = new Square();
        var player = new Player();
        bottom.ConnectSquare(Position.Top, top);

        top.SetLine(Position.Bottom, player);

        Assert.Equal(
            top.GetLineOwner(Position.Bottom),
            bottom.GetLineOwner(Position.Top)
        );
    }


    [Fact]
    public void Squares_CantConnectOpposingSides()
    {
        var top = new Square();
        var bottom = new Square();
        bottom.ConnectSquare(Position.Top, top);
        Assert.Throws<AlreadyConnected>(() =>
            top.ConnectSquare(Position.Bottom, bottom));
    }

    [Fact]
    public void SettingLine_OnCompleteSquare_MakesWinner()
    {
        var square = new Square();
        var player = new Player();

        square.SetLine(Position.Top, player);
        square.SetLine(Position.Bottom, player);
        square.SetLine(Position.Left, player);

        Assert.True(square.SetLine(Position.Right, player));
        Assert.Equal(player, square.Owner);
    }
}