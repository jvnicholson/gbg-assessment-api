using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using GeneByGene.Api.Repositories;
using Microsoft.Practices.Unity;
using Newtonsoft.Json.Serialization;

namespace GeneByGene.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            HttpConfiguration config = GlobalConfiguration.Configuration;

            Register(config);
        }

        public static void Register(HttpConfiguration config)
        {
            // Initialize DI
            var container = new UnityContainer();

            container.RegisterType<IUsersRepository, UsersRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IStatusesRepository, StatusesRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ISamplesRepository, SamplesRepository>(new HierarchicalLifetimeManager());

            config.DependencyResolver = new UnityResolver(container);

            // Set camelCase for JSON output
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;
        }
    }
}
