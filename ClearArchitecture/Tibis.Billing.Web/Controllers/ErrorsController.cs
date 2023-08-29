using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Security.Authentication;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Tibis.Domain;

namespace Tibis.Billing.Web.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
public sealed class ErrorsController : ControllerBase
{
    [Route("error")]
    public ProblemDetails Error()
    {
        // The documentation says the return value can be null, even though the return value isn't marked as nullable.
        var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

        // ReSharper thinks that context is non-nullable, so we suppress its warning about the use of '?.'
        // ReSharper disable once ConstantConditionalAccessQualifier
        var exception = context?.Error ?? new TibisException("Routed to error controller, but no exception has been thrown"); // Your exception
        if (exception is AggregateException ae)
            exception = ae.InnerException ?? ae;

        var code = exception switch {
            ItemNotFoundException => 404, // Not Found
            AuthenticationException => 401, // Unauthorized
            //LockedUserException => 403, // Forbidden (e.g. credentials are valid but user is locked)
            //ExpiredPasswordException => 419, // Authentication Timeout
            ItemAlreadyExistsException => 409, // Conflict
            ValidationException => 400, // Bad Request
            TibisValidationException => 400, // Bad Request
            ArgumentOutOfRangeException => 400, // Bad Request
            _ => 500, // Internal Server Error by default
        };

        Response.StatusCode = code; // You can use HttpStatusCode enum instead

        return CreateProblemDetails(exception, code);
    }

    private static ProblemDetails CreateProblemDetails(Exception baseException, int statusCode)
    {
        var pd = new ProblemDetails
        {
            Title = baseException.GetType().Name,
            Detail = baseException.Message,
            Status = statusCode,
        };

        var stackTrace = baseException.StackTrace;
        if (stackTrace != null)
            pd.Extensions["StackTrace"] = stackTrace;

        foreach (DictionaryEntry entry in baseException.Data)
            pd.Extensions.Add(entry.Key.ToString()!, entry.Value);

        return pd;
    }
}