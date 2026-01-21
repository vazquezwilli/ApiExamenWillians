using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Security.Claims;
namespace Infrastructure.Middlewares
{
    public class RequestLogMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestLogMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ApplicationDbContext dbContext)
        {
            var watch = Stopwatch.StartNew();
            string mensajeError = string.Empty;

            try
            {
                await _next(context); 
            }
            catch (Exception ex)
            {
                mensajeError = ex.InnerException != null
                    ? $"{ex.Message} | INNER: {ex.InnerException.Message}"
                    : ex.Message;

                Console.WriteLine($"FALLO DETECTADO: {mensajeError}");

                context.Response.StatusCode = 500;

                throw;
            }
            finally
            {
                watch.Stop();

                var log = new LogRequest
                {
                    Metodo = context.Request.Method,
                    Ruta = context.Request.Path,
                    Usuario = context.User.Identity?.Name ?? "Anónimo",
                    Fecha = DateTime.Now,
                    Ip = context.Connection.RemoteIpAddress?.ToString() ?? "0.0.0.0",
                    StatusCode = context.Response.StatusCode,
                    DuracionMs = (int)watch.ElapsedMilliseconds,
                    Error = mensajeError  
                };

                try
                {
                    dbContext.Logs.Add(log);
                    await dbContext.SaveChangesAsync();
                }
                catch (Exception dbEx)
                {
                    Console.WriteLine("Error crítico guardando el log: " + dbEx.Message);
                }
            }
        }
    }
}
