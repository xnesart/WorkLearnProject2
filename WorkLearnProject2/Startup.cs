using System.Web.Http;
using Owin;

namespace WorkLearnProject2
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseWelcomePage("/");
            app.Use(typeof(ErrorHandlingModule)); 
            app.Use(typeof(LoggerModule), "OwinLogger: ");
            app.Use(typeof(WebApiModule));
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute("default", routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
            app.UseWebApi(config);
        }
    }
}