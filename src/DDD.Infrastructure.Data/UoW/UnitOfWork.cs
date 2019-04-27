using System;
using DDD.Infrastructure.Data.Context;

namespace DDD.Infrastructure.Data.UoW
{
    public class UnitOfWork : IUnitOfWork // Esta DI precisa ser resolvida em DDD.Infrastructure.CrossCutting.IoC.BootStraper.cs 
    {
        public bool _disposed;
        private readonly EFContext _context;

        // Esta DI precisa ser resolvida em DDD.Infrastructure.CrossCutting.IoC.BootStraper.cs 
        //Está injetando a dependência da classe e não da interface (O EFContext não provê uma interface, tudo bem.)
        public UnitOfWork(EFContext context)  
        {
            _context = context;
            _disposed = false;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing) _context.Dispose();  //Só executa uma vez para não ocorrer NullReference
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
