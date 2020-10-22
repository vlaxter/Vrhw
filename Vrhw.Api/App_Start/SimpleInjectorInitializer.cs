using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using System.Web.Http;
using Vrhw.Core.Services;
using Vrhw.Repository.Memory;
using Vrhw.Repository.Sql;
using Vrhw.Shared.Interfaces;

namespace Vrhw.Api.App_Start
{
    public class SimpleInjectorInitializer
    {
        /// <summary>
        ///     Initializes injector
        /// </summary>
        public static void Initialize()
        {
            var container = new Container();

            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();

            container.Register<IDiffService, DiffService>(Lifestyle.Scoped);
            //container.Register<IDiffRepository, SqlRepository>(Lifestyle.Scoped);
            container.Register<IDiffRepository, MemoryRepository>(Lifestyle.Scoped);

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }
    }
}