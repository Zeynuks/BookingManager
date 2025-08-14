using Domain.Enums;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace BookingManager.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ErrorHandlingMiddleware( RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger,
            IHostEnvironment env )
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task Invoke( HttpContext context )
        {
            try
            {
                await _next( context );
            }
            catch ( AppException ex )
            {
                int status = ex.Code switch
                {
                    ErrorCode.NotFound => StatusCodes.Status404NotFound,
                    ErrorCode.Validation => StatusCodes.Status400BadRequest,
                    ErrorCode.Conflict => StatusCodes.Status409Conflict,
                    ErrorCode.Forbidden => StatusCodes.Status403Forbidden,
                    ErrorCode.Unauthorized => StatusCodes.Status401Unauthorized,
                    ErrorCode.Unavailable => StatusCodes.Status503ServiceUnavailable,
                    _ => StatusCodes.Status500InternalServerError
                };

                ProblemDetails problem = new()
                {
                    Status = status,
                    Title = ex.Code.ToString()
                };

                if ( _env.IsDevelopment() )
                {
                    problem.Detail = ex.Message;
                }

                context.Response.StatusCode = status;
                context.Response.ContentType = "application/problem+json";
                await context.Response.WriteAsJsonAsync( problem );
            }
            catch ( Exception ex )
            {
                _logger.LogError( ex, "Unhandled" );

                ProblemDetails problem = new()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Internal Server Error"
                };

                if ( _env.IsDevelopment() )
                {
                    problem.Detail = ex.Message;
                }

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/problem+json";
                await context.Response.WriteAsJsonAsync( problem );
            }
        }
    }
}