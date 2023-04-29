using DotGameLogic;
using DotGameLogic.Exceptions;

namespace DotGameLogic_Test.BoardTests;

[TestClass]
public class BoardTest
{
    [TestMethod]
    public void NewBoard_CorrectBoardSize()
    {
        Assert.AreEqual(
            4,
            new Board(2, 2).BoardState.Count
        );

        Assert.AreEqual(
            6,
            new Board(2, 3).BoardState.Count
        );

        Assert.AreEqual(
            9,
            new Board(3, 3).BoardState.Count
        );
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidBoardSize))]
    public void NewBoard_MinimunSize()
    {
        var _ = new Board(1, 1);
    }
}