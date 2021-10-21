using System.Collections.Generic;
using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace SwaggerBug
{
    public static class Function1
    {
        [OpenApiOperation(operationId: "Function1", tags: new[] { "Function" }, Summary = "Show ",Visibility = OpenApiVisibilityType.Important)]
        [OpenApiParameter("filter", Type = typeof(string), In = ParameterLocation.Path, Visibility = OpenApiVisibilityType.Important, Description = "Optional string", Required =false )]
        [Function("Function1")]
        public static HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "MyRoute/{filter?}")] HttpRequestData req,
            FunctionContext executionContext, string filter)
        {
            var logger = executionContext.GetLogger("Function1");
            logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString($"The filter value is {(filter==null?"null":filter)}");

            return response;
        }
    }
}
