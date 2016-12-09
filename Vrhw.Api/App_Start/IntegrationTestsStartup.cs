using Microsoft.Owin;
using Owin;
using SimpleInjector;
using SimpleInjector.Extensions.ExecutionContextScoping;
using SimpleInjector.Integration.WebApi;
using System.Web.Http;
using Vrhw.Core.Services;
using Vrhw.Repository.Memory;
using Vrhw.Shared.Interfaces;

[assembly: OwinStartup(typeof(Vrhw.Api.IntegrationTests.IntegrationTestsStartup))]

namespace Vrhw.Api.IntegrationTests
{
    /// <summary>
    /// Configuration to run the Integration tests in an application hosted in memory
    /// </summary>
    public class IntegrationTestsStartup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            // Dependency Inyections
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new ExecutionContextScopeLifestyle();
            container.Register<IDiffService, DiffService>(Lifestyle.Scoped);
            //container.Register<IDiffRepository, SqlRepository>(Lifestyle.Scoped);
            container.Register<IDiffRepository, MemoryRepository>(Lifestyle.Scoped);
            container.Verify();

            appBuilder.Use(async (context, next) =>
            {
                using (container.BeginExecutionContextScope())
                {
                    await next();
                }
            });

            // Mappings for the in memory host
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute("DefaultApi", "v1/{controller}/{id}/{action}", new { id = RouteParameter.Optional, action = RouteParameter.Optional });
            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);  // https://github.com/simpleinjector/SimpleInjector/issues/138

            appBuilder.UseWebApi(config).UseNancy();
        }
    }
}