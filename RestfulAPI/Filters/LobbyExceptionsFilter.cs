using DotGameOrleans.Grains.Lobby;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RestfulAPI.Filters;

/// <summary>
/// Error handling for <see cref="LobbyException"/>.
/// </summary>
public class LobbyExceptionsFilter : IActionFilter, IOrderedFilter
{
    /// <inheritdoc />
    public int Order => int.MaxValue - 10;

    /// <inheritdoc />
    public void OnActionExecuting(ActionExecutingContext context)
    {
    }

    /// <inheritdoc />
    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception is not LobbyException) return;

        context.Result = context.Exception switch
        {
            LobbyNotInitialized => new NotFoundObjectResult(nameof(LobbyNotInitialized)),
            NotInLobby => new UnauthorizedObjectResult(nameof(NotInLobby)),
            LobbyAlreadyJoined => new ConflictObjectResult(nameof(LobbyAlreadyJoined)),
            LobbyInProgress => new ConflictObjectResult(nameof(LobbyInProgress)),
            NotEnoughPlayers => new ConflictObjectResult(nameof(NotEnoughPlayers)),
            LobbyNotInProgress => new ConflictObjectResult(nameof(LobbyNotInProgress)),
            _ => throw context.Exception
        };

        context.ExceptionHandled = true;
    }
}