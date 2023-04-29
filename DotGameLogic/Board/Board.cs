using DotGameLogic.Board.Exceptions;

namespace DotGameLogic.Board;

/// <summary>
/// The main class for a DotGame Board
/// </summary>
public class Board
{
    public int Height { get; }
    public int Width { get; }

    public int TotalSquares => Height * Width;

    public List<Square> BoardState { get; } = new();

    public Board(int height, int width)
    {
        if (height <= 1 || width <= 1)
            throw new InvalidBoardSize();
        
        Height = height;
        Width = width;
        
        ResetBoard();
    }

    /// <summary>
    /// Cleans the board, creates a new one with the correct
    /// height and width and connects the squares.
    /// </summary>
    public void ResetBoard()
    {
        for (var h = 0; h < Height; h++)
        {
            Square? leftSquare = null;

            for (var w = 0; w < Width; w++)
            {
                Square newSquare = new();
                
                // Check if has square to the left
                if(leftSquare != null)
                    newSquare.ConnectSquare(Position.Left, leftSquare);
                
                // Check if has square above
                if (h > 0)
                {
                    var topSquare = BoardState[(h - 1) * Width + w];
                    newSquare.ConnectSquare(Position.Top, topSquare);
                }
                
                BoardState.Add(newSquare);
            }
        }
    }
}