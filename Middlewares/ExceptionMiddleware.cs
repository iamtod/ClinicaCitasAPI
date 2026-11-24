using System.Text.Json;
using ClinicaCitasAPI.DTOs;
using ClinicaCitasAPI.Exceptions;

namespace ClinicaCitasAPI.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (BusinessException ex)
        {
            await EscribirError(context, ex.StatusCode, ex.Message);
        }
        catch (Exception)
        {
            await EscribirError(context, 500, "Error interno del servidor.");
        }
    }

    private static Task EscribirError(HttpContext context, int statusCode, string mensaje)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;
        var respuesta = ApiResponse<object>.Error(mensaje);
        return context.Response.WriteAsync(JsonSerializer.Serialize(respuesta));
    }
}