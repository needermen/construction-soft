using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Cs.Application.Tools;
using Cs.Service.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Cs.Service.Configuration.Middlewares
{
    public class LogAndExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public LogAndExceptionHandler(RequestDelegate next,
            ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<LogAndExceptionHandler>();
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ServiceException ex)
            {
                var result = ServiceResult<object>.Fail(ex.Message);

                context.Response.StatusCode = (int) HttpStatusCode.OK;

                context.Response.ContentType = "application/json";

                var responseJson = JsonConvert.SerializeObject(result, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });

                await context.Response.WriteAsync(responseJson);
            }
            catch (Exception ex)
            {
                var guid = Guid.NewGuid();

                var result = ServiceResult<bool>.Error($"დაფიქსირდა შეცდომა, შეცდომის კოდი: {guid} გაუგზავნეთ ადმინისტრატორს");

                context.Response.StatusCode = (int) HttpStatusCode.OK;

                context.Response.ContentType = "application/json";

                var responseJson = JsonConvert.SerializeObject(result, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });

                await context.Response.WriteAsync(responseJson);

                _logger.LogError(guid.ToString());
                _logger.LogError(await FormatRequest(context.Request));
                _logger.LogError($"Response: {responseJson}");
                _logger.LogError(ex.ToString());
            }
        }

        private async Task<string> FormatRequest(HttpRequest request)
        {
            var body = request.Body;
            request.EnableRewind();

            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            var bodyAsText = Encoding.UTF8.GetString(buffer);
            request.Body = body;

            return $"{request.Scheme} {request.Host} {request.Path} {request.QueryString} {bodyAsText}";
        }
    }
}