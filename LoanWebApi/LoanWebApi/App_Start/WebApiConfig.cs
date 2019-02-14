using System.Web.Http;
using LoanWebApi.ActionFIlters;
using Newtonsoft.Json.Serialization;

namespace LoanWebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Filters.Add(new LoggingFilterAttribute());
            config.Filters.Add(new GlobalExceptionAttribute());

            // Web API configuration and services
            config.EnableCors();

            // JSON object format in camel casing
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = 
                new CamelCasePropertyNamesContractResolver();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{accountNo}",
                defaults: new { accountNo = RouteParameter.Optional }
            );
        }
    }
}
