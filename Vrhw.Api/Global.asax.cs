using System.Web.Http;
using Vrhw.Api.App_Start;

namespace Vrhw.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            SimpleInjectorInitializer.Initialize();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}