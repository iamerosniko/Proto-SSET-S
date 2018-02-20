using System.Web.Http;

using System.Web.Http.Cors;
namespace API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var json = config.Formatters.JsonFormatter;

            json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.MessageHandlers.Add(new TokenValidationHandler());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            //install Microsoft.AspNet.WebApi.Cors package first using nuget package management
            //enabling cors
            EnableCorsAttribute cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
            //for chrome to enable preflight request 
            //--disable-web-security --user-data-dir

            //to filter the cors
            //import System.Web.Http.Cors to every controller you want to filter
            //add annotation this to controller's class
            //[EnableCorsAttribute("*","*","*")] 
            //to disable some httpmethods 
            //add annotation to method
            //[DisableCors] 
        }
    }
}
