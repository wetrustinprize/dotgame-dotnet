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
            new Board(new BoardConfig
            {
                Height = 2,
                Width = 2
            }).BoardState.Count
        );

        Assert.Equal(
            6,
            new Board(new BoardConfig
            {
                Height = 2,
                Width = 3
            }).BoardState.Count
        );

        Assert.Equal(
            9,
            new Board(new BoardConfig
            {
                Height = 3,
                Width = 3
            }).BoardState.Count
        );
    }

    [Fact]
    public void NewBoard_MinimunSize()
    {
        Assert.Throws<InvalidBoardSize>(() => new Board(new BoardConfig
        {
            Height = 1,
            Width = 1
        }));
    }

    [Fact]
    public void BoardPosition_OutOfBounds()
    {
        var board = new Board(new BoardConfig
        {
            Height = 2,
            Width = 2
        });

        Assert.Throws<InvalidBoardPositionException>(() => board.GetIndex(2, 3));
        Assert.Throws<InvalidBoardPositionException>(() => board.GetIndex(4, 3));
        Assert.Throws<InvalidBoardPositionException>(() => board.GetIndex(3, 2));
    }
}