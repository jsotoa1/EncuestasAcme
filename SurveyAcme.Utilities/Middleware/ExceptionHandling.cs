using SurveyAcme.Models.Outputs;
using SurveyAcme.Models.Utilities;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;

namespace SurveyAcme.Utilities.Middleware
{
    public class ExceptionHandling
    {
        private readonly RequestDelegate _next;
        public ExceptionHandling(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var response = context.Response;
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;

            GetAttributes(exception, out ResultOut errorResponse, ref statusCode);

            response.StatusCode = (int)statusCode;

            var settings = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                },
                NullValueHandling = NullValueHandling.Ignore,
            };

            var result = JsonConvert.SerializeObject(errorResponse, settings);
            await context.Response.WriteAsync(result);
        }

        private void GetAttributes(Exception exception, out ResultOut errorResponse, ref HttpStatusCode statusCode)
        {
            switch (exception)
            {
                case CustomException ex:
                    errorResponse = new(ex.Message, ex.TechnicalError, ex.FieldValidations);
                    statusCode = ex.StatusCode;
                    break;
                default:
                    errorResponse = new(exception.Message, exception.ToString(), null);
                    statusCode = HttpStatusCode.InternalServerError;
                    break;
            }
        }
    }
}