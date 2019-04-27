using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DDD.Domain.Entity;
using DDD.Domain.Interfaces.Repository;
using DDD.Infrastructure.Data.Context;

namespace DDD.Infrastructure.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected EFContext Db;
        protected DbSet<TEntity> DbSet;

        //(SEM UoW)
        //==============================
        //Avançado I 02:23:10
        /*
        public Repository()
        {
            Db = new EFContext(); 
            DbSet = Db.Set<TEntity>();
        }
        */
        //==============================

        //(COM UoW) 
        public Repository(EFContext context)
        {
            //Recebe uma instância pronta registrada lá no BootStrapper => container.Register<EFContext>(Lifestyle.Scoped);
            //Isso quebra o ClienteRepository (02:24:19) que então, precisa receber a injeção de dependência
            Db = context;
            DbSet = Db.Set<TEntity>();
        }

        public virtual TEntity Adicionar(TEntity obj)
        {
            var objReturn = DbSet.Add(obj);
            //SaveChanges(); Retirado para implementar o UOW
            return objReturn;
        }

        public virtual TEntity ObterPorId(Guid id)
        {
            return DbSet.Find(id);
        }

        public virtual IEnumerable<TEntity> ObterTodos() //(int t, int s)
        {
            return DbSet.ToList(); //Take(t).Skip(s).ToList();
        }

        public virtual TEntity Atualizar(TEntity obj)
        {
            var entry = Db.Entry(obj);
            DbSet.Attach(obj);
            entry.State = EntityState.Modified;
            SaveChanges(); //Retirar para implementar o UOW mas observe como foi fei o o Adicionar com UoW

            return obj;
        }

        public virtual void Remover(Guid id)
        {
            DbSet.Remove(DbSet.Find(id));
            //SaveChanges(); Retirado para implementar o UOW
        }

        public IEnumerable<TEntity> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public int SaveChanges()
        {
            return Db.SaveChanges();
        }

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}