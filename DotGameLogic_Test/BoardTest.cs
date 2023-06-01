using DotGameLogic;
using DotGameLogic.Exceptions;

namespace DotGameLogic_Test;

public class BoardTest
{
    [Fact]
    public void NewBoard_CorrectBoardSize()
    {
        Assert.Equal(
            4,
            new Board(2, 2).BoardState.Count
        );

        Assert.Equal(
            6,
            new Board(2, 3).BoardState.Count
        );

        Assert.Equal(
            9,
            new Board(3, 3).BoardState.Count
        );
    }

    [Fact]
    public void NewBoard_MinimunSize()
    {
        Assert.Throws<InvalidBoardSize>(() => new Board(1, 1));
    }
}