using System;
using System.Web.Mvc;  //References => System.Web.Mvc

namespace DDD.Infrastructure.CrossCutting.MvcFilters
{
    public class GlobalErrorHandler : ActionFilterAttribute    // Obter em NUGET - Microsoft.AspNet.MVC e referenciar => System.Web.Mvc
    {
        //AQUI É O PILELINE DO AST.Net - Usar com moderação senão a aplicação pode ficar lenta.

        //Pode-se escrever uma classe para LOG e Injetar dependência aqui.
        //private readonly ILogger _logger;
        //public GlobalErrorHandler(ILogger logger)
        //{
        //    _logger = logger;
        //}

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
           
            if(filterContext.Exception != null)
            {
                //_logger.log(filterContext.Exception);
                filterContext.Controller.TempData["ErrorCode"] = "EITAAAA: Erro capturado em DDD.Infrastructure.CrossCutting.MvcFilters.GlobalErrorHandler.OnActionExecuted - Conteudo:" + filterContext.Exception.ToString();
                //Aqui pode ser feito um LOG por exemplo
            }
            base.OnActionExecuted(filterContext);
    }
    }
}
/*
 Para Funcionar:
 1) Na Camada de Apresentação referenciar o projeto DDD.Infrastructure.CrossCutting.MvcFilters
 2) No App_Start em DDD.MVC Classe FilterConfig.cs, método RegisterGlobalFilters  =>  Adicionar a linha => filters.Add(new GlobalErrorHandler());  
 3) Atentar para => using DDD.Infrastructure.CrossCutting.MvcFilters;
*/
