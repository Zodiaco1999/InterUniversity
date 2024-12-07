using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;
using Newtonsoft.Json.Serialization;
using UniversityApi.Common.Models;
using UniversityApi.Common.Exceptions;
using Microsoft.Data.SqlClient;
using UniversityApi.Common.ContextAccesor;

namespace UniversityApi.Common.Middlewares;

public class CustomExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public CustomExceptionMiddleware(RequestDelegate next, ILogger<HttpContext> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            try
            {
                var body = await new StreamReader(context.Request.Body).ReadToEndAsync();

                context.Request.Body.Position = 0;

                var json = JsonConvert.SerializeObject(new { path = context.Request.Path.Value, userid = context.User.Identity?.Name, method = context.Request.Method, remoteIp = $"{context.Connection.RemoteIpAddress}:{context.Connection.RemotePort}", body });

                _logger.LogInformation("Invoke: {0}", json);
            }
            catch (Exception e)
            {
                _logger.LogInformation(e, "Error leyendo 'Request.Body'");
            }

            await _next.Invoke(context);
        }
        catch (Exception ex)
        {
   
            try
            {
                using (var reader = new StreamReader(context.Request.Body))
                {
                    var body = reader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Message: {@Message}. Description: {@description}. Host: {@Host}. Path: {@Path}. UserId: {@UserId}. Method: {@Method}. RemoteIpAddress: {@RemoteIpAddress}", ex.Message, "Error leyendo 'Request.Body'", context.Request.Host.Value, context.Request.Path.Value, context.User.Identity?.Name, context.Request.Method, context.Connection?.RemoteIpAddress?.ToString());
            }
            
            await HandleExceptionAsync(context, ex);

        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var response = context.Response;

        var json = JsonConvert.SerializeObject(new { path = context.Request.Path.Value, userid = context.User.Identity?.Name, method = context.Request.Method, remoteIp = $"{context.Connection.RemoteIpAddress}:{context.Connection.RemotePort}" });

        CustomErrorResponse customError = new CustomErrorResponse();

        if (exception is DbUpdateException dbUpdateException && dbUpdateException.InnerException is SqlException sqlException && sqlException.Number == 547)
        {
            customError.StatusCode = (int)HttpStatusCode.BadRequest;
            customError.TypeException = TypeException.Error;
            customError.Message = $"Se generó un conflicto con una restricción asociada a la entidad. No se pudo finalizar la transacción";
            customError.IsWarning = true;

            _logger.LogError(exception, "Message: {@Message}. Description: {@description}. Host: {@Host}. Path: {@Path}. UserId: {@UserId}. Method: {@Method}. RemoteIpAddress: {@RemoteIpAddress}", customError.TypeException, customError.Message, context.Request.Host.Value, context.Request.Path.Value, context.User.Identity?.Name, context.Request.Method, context.Connection.RemoteIpAddress?.ToString());
        }
        else if (exception is BaseException customException)
        {
            customError.StatusCode = customException.Code;
            customError.TypeException = TypeException.Error;
            customError.Message = customException.Message;

            if (customException.IsWarning)
            {
                 customError.TypeException = TypeException.Warning;
                _logger.LogWarning(customException, $"{customException.Description}. {json}");
            }
            else
            {
                _logger.LogError(exception, "Message: {@Message}. Description: {@description}. Host: {@Host}. Path: {@Path}. UserId: {@UserId}. Method: {@Method}. RemoteIpAddress: {@RemoteIpAddress}", customError.TypeException, $"{(string.IsNullOrEmpty(customError.Message) ? "" : customError.Message + ", ") }{exception.InnerException?.Message}" , context.Request.Host.Value, context.Request.Path.Value, context.User.Identity?.Name, context.Request.Method, context.Connection.RemoteIpAddress?.ToString());
            }
        }
        else if (exception is ValidationException validationException)
        {
            customError.StatusCode = (int)HttpStatusCode.NotAcceptable;
            customError.TypeException = TypeException.Warning;
            customError.Message = validationException.Message;
            customError.IsWarning = true;

            _logger.LogWarning(exception, "Message: {@Message}. Description: {@description}. Host: {@Host}. Path: {@Path}. UserId: {@UserId}. Method: {@Method}. RemoteIpAddress: {@RemoteIpAddress}", customError.TypeException, customError.Message, context.Request.Host.Value, context.Request.Path.Value, context.User.Identity?.Name, context.Request.Method, context.Connection?.RemoteIpAddress?.ToString());
        }
        else if (exception is NotFoundException)
        {
            customError.StatusCode = (int)HttpStatusCode.BadRequest;
            customError.TypeException = TypeException.Error;
            customError.Message = exception.Message;

            _logger.LogError(exception, "Message: {@Message}. Description: {@description}. Host: {@Host}. Path: {@Path}. UserId: {@UserId}. Method: {@Method}. RemoteIpAddress: {@RemoteIpAddress}", customError.TypeException, customError.Message, context.Request.Host.Value, context.Request.Path.Value, context.User.Identity?.Name, context.Request.Method, context.Connection?.RemoteIpAddress?.ToString());
        }
        else
        {
            customError.StatusCode = (int)HttpStatusCode.InternalServerError;
            customError.TypeException = TypeException.Error;
            customError.Message = "Intente de nuevo, si el problema persiste contacte con soporte";

            _logger.LogError(exception, "Message: {@Message}. Description: {@description}. Host: {@Host}. Path: {@Path}. UserId: {@UserId}. Method: {@Method}. RemoteIpAddress: {@RemoteIpAddress}", exception.Message, exception.InnerException?.Message, context.Request.Host.Value, context.Request.Path.Value, context.User.Identity?.Name, context.Request.Method, context.Connection?.RemoteIpAddress?.ToString());
        }

        response.ContentType = "application/json";
        response.StatusCode = customError.StatusCode;

        await response.WriteAsync(JsonConvert.SerializeObject(customError, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }));
    }
}
