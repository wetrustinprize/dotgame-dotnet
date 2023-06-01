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
        bottom.ConnectSquare(Position.Top, top);

        top.SetLine(Position.Bottom, 1);

        Assert.Equal(
            top.GetLine(Position.Bottom),
            bottom.GetLine(Position.Top)
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

        square.SetLine(Position.Top, 1);
        square.SetLine(Position.Bottom, 1);
        square.SetLine(Position.Left, 1);

        Assert.True(square.SetLine(Position.Right, 1));
        Assert.Equal(1, square.Owner);
    }
}