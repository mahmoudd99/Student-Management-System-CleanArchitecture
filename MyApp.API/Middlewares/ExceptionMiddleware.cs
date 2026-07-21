using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using FluentValidation;
using Serilog;

namespace MyApp.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }

            catch(Exception ex)
{
                Log.Error(ex, "Unhandled exception occurred");

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                var response = new
                {
                    Success = false,
                    Message = ex.Message
                };

                await context.Response.WriteAsJsonAsync(response);
            }
            ;
            }
        }
        }
    