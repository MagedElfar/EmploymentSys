using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UntitOfWork : IUnitOfWork
    {
        private readonly AdbContext context;
        private Hashtable repositories;
        public UntitOfWork(AdbContext context)
        {
            this.context = context;
        }
        public async Task<int> Compleate()
        {
            return await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class, IBaseEntity
        {
            if (repositories == null) repositories = new Hashtable();

            var type = typeof(TEntity);
            var typeName = type.Name;

            if (!repositories.ContainsKey(typeName))
            {

                var repoType = typeof(GenericRepository<>);
                var repoInstance = Activator.CreateInstance(repoType.MakeGenericType(type), context);

                repositories.Add(typeName, repoInstance);

            }


            return (IGenericRepository<TEntity>)repositories[typeName];
        }
    }
}
