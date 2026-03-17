using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using HotelBooking.Application.Common.Exceptions;

namespace HotelBooking.Api.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IHostEnvironment _env;

    public ExceptionMiddleware(
        RequestDelegate next,
        ILogger<ExceptionMiddleware> logger,
        IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception occurred.");
            await HandleExceptionAsync(context, ex, _env.IsDevelopment());
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception ex, bool isDevelopment)
    {
        var statusCode = ex switch
        {
            BadRequestException => StatusCodes.Status400BadRequest,
            NotFoundException => StatusCodes.Status404NotFound,
            ConflictException => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };

        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = statusCode;

        var problem = new ProblemDetails
        {
            Type = $"https://httpstatuses.com/{statusCode}",
            Title = GetTitle(statusCode),
            Status = statusCode,
            Detail = isDevelopment ? ex.Message : GetDefaultDetail(statusCode),
            Instance = context.Request.Path
        };

        var json = JsonSerializer.Serialize(problem);
        await context.Response.WriteAsync(json);
    }

    private static string GetTitle(int statusCode) => statusCode switch
    {
        400 => "Bad Request",
        404 => "Not Found",
        409 => "Conflict",
        _ => "Internal Server Error"
    };

    private static string GetDefaultDetail(int statusCode) => statusCode switch
    {
        400 => "The request is invalid.",
        404 => "The requested resource was not found.",
        409 => "A conflict occurred while processing the request.",
        _ => "An unexpected error occurred."
    };
}