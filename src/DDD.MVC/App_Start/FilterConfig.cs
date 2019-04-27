using DDD.Infrastructure.CrossCutting.MvcFilters;
using System.Web;
using System.Web.Mvc;

namespace DDD.MVC
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new GlobalErrorHandler());  //Atentar para o using
        }
    }
}
