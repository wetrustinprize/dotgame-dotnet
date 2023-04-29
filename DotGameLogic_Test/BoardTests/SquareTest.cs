using DotGameLogic;
using DotGameLogic.Enums;
using DotGameLogic.Exceptions;

namespace DotGameLogic_Test.BoardTests;

[TestClass]
public class SquareTest
{
    [TestMethod]
    public void ConnectedSquares_ShareLines()
    {
        var top = new Square();
        var bottom = new Square();
        bottom.ConnectSquare(Position.Top, top);

        top.SetLine(Position.Bottom, 1);
        
        Assert.AreEqual(
            top.GetLine(Position.Bottom),
            bottom.GetLine(Position.Top)
        );
    }
    
    [TestMethod]
    [ExpectedException(typeof(AlreadyConnected))]
    public void Squares_CantConnectOpposingSides()
    {
        var top = new Square();
        var bottom = new Square();
        bottom.ConnectSquare(Position.Top, top);
        top.ConnectSquare(Position.Bottom, bottom);
    }

    [TestMethod]
    public void SettingLine_OnCompleteSquare_MakesWinner()
    {
        var square = new Square();
        
        square.SetLine(Position.Top, 1);
        square.SetLine(Position.Bottom, 1);
        square.SetLine(Position.Left, 1);
        
        Assert.IsTrue(square.SetLine(Position.Right, 1));
        Assert.AreEqual(1, square.Owner);
    }
}