using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using SimpleInjector;
using SimpleInjector.Extensions.ExecutionContextScoping;
using Vrhw.Shared.Interfaces;
using Vrhw.Core.Services;
using Vrhw.Repository.Memory;
using SimpleInjector.Integration.WebApi;
using Vrhw.Repository.Sql;

[assembly: OwinStartup(typeof(Vrhw.Api.IntegrationTests.Startup))]

namespace Vrhw.Api.IntegrationTests
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
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
            
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute("DefaultApi", "v1/{controller}/{id}/{action}", new { id = RouteParameter.Optional, action = RouteParameter.Optional });
            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);  // https://github.com/simpleinjector/SimpleInjector/issues/138

            appBuilder.UseWebApi(config).UseNancy();
        }
    }
}
