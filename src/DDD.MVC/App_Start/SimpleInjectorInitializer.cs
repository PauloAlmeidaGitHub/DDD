using System.Reflection;
using System.Web.Mvc;
using DDD.Infrastructure.CrossCutting.IoC;
using DDD.MVC.App_Start;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using WebActivator;

[assembly: WebActivator.PostApplicationStartMethod(typeof(DDD.MVC.App_Start.SimpleInjectorInitializer), "Initialize")]

namespace DDD.MVC.App_Start
{
    public static class SimpleInjectorInitializer
    {
        /// <summary>Initialize the container and register it as MVC3 Dependency Resolver.</summary>
        public static void Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            InitializeContainer(container);
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            container.Verify();
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
     
        private static void InitializeContainer(Container container)
        {

            // For instance:
            // container.Register<IUserRepository, SqlUserRepository>(Lifestyle.Scoped
            //O trecho acima é substituido pela linha abaixo que exige a Referêncioa a DDD.Infrastructure.CrossCutting.IoC
            BootStrapper.RegisterServices(container);
        }
    }
}