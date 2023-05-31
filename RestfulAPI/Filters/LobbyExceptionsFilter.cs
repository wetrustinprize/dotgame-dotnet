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
            LobbyNotInitializedException => new NotFoundObjectResult(nameof(LobbyNotInitializedException)),
            NotInLobbyException => new UnauthorizedObjectResult(nameof(NotInLobbyException)),
            LobbyAlreadyJoinedException => new ConflictObjectResult(nameof(LobbyAlreadyJoinedException)),
            NotEnoughPlayersException => new ConflictObjectResult(nameof(NotEnoughPlayersException)),
            _ => throw context.Exception
        };

        context.ExceptionHandled = true;
    }
}