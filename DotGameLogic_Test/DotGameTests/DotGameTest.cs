using DotGameLogic.DotGame;
using DotGameLogic.DotGame.Exceptions;

namespace DotGameLogic_Test.DotGameTests;

[TestClass]
public class DotGameTest
{
    [TestMethod]
    [ExpectedException(typeof(NotEnoughPlayers))]
    public void NewGame_WithNotEnoughPlayers_RaisesException()
    {
        var _ = new DotGame(1, 1, 0);
    }
}