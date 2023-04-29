using DotGameLogic.Board;
using DotGameLogic.Board.Exceptions;

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
}