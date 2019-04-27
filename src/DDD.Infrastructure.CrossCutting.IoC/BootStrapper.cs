
using DDD.Application;
using DDD.Application.Interfaces;
using DDD.Domain.Interfaces.Repository;
using DDD.Domain.Interfaces.Services;
using DDD.Domain.Services;
using DDD.Infrastructure.Data.Context;
using DDD.Infrastructure.Data.Repository;
using DDD.Infrastructure.Data.UoW;
using SimpleInjector;

namespace DDD.Infrastructure.CrossCutting.IoC  //Criar Referências para DDD.Application, DDD.Domain e DDD.Infrastructure.Data
{
    public class BootStrapper
    {
        public static void RegisterServices(Container container)
        {
            // Lifestyle.Transient  => Uma instancia para cada solicitacao;
            // Lifestyle.Singleton  => Uma instancia unica para a classe  (Perigoso)
            // Lifestyle.Scoped     => Uma instancia unica para o request   (Recomendada e Default)

            // App
            container.Register<IClienteApplicationService, ClienteApplicationService>(Lifestyle.Scoped);

            // Domain
            container.Register<IClienteService, ClienteService>(Lifestyle.Scoped);

            // Infrastructure
            container.Register<IClienteRepository, ClienteRepository>(Lifestyle.Scoped);

            container.Register<IUnitOfWork, UnitOfWork>(Lifestyle.Scoped);  //Ver UnitOfWork.cs
            container.Register<EFContext>(Lifestyle.Scoped);                //Ver UnitOfWork.cs  (O EF precisa estar instalado em DDD.Infrastructure.CrossCutting.IoC)

        }
    }
}