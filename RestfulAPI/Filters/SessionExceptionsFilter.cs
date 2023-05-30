using DotGameOrleans.Grains.Session;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RestfulAPI.Filters;

/// <summary>
/// Error handling for <see cref="SessionException"/>.
/// </summary>
public class SessionExceptionsFilter : IActionFilter, IOrderedFilter
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
        if (context.Exception is not SessionException) return;

        context.Result = context.Exception switch
        {
            SessionNotInitialized => new UnauthorizedObjectResult(nameof(SessionNotInitialized)),
            _ => throw context.Exception
        };

        context.ExceptionHandled = true;
    }
}