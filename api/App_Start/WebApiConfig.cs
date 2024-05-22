using System.Web.Http;
using System.Web.Http.Cors;

namespace DigitalMicrowave
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var policy = new EnableCorsAttribute("http://localhost:5173", "*", "*");
            policy.SupportsCredentials = true;
            config.EnableCors(policy);

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DigitalMicrowave",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
