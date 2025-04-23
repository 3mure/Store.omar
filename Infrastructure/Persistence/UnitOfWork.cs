using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contract;
using Domain.Models;
using Persistence.Data;
using Persistence.Repository;

namespace Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbcontext _context;
        //private readonly Dictionary<string, object> _repositoryies;
        private readonly ConcurrentDictionary<string, object> _repositoryies;
        public UnitOfWork(StoreDbcontext context)
        {
            _context = context;
            //_repositoryies = new Dictionary<string, object>();
            _repositoryies = new ConcurrentDictionary<string, object>();
        }
        public IGenaricRepository<TEntity, Tkey> GetRepository<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>
        {
            //var type = typeof(TEntity).Name;
            //if (!_repositoryies.ContainsKey(type))
            //{ 
            // var repository = new  GenaricRepository<TEntity, Tkey>(_context);
            //    _repositoryies.Add(type, repository);
            //}
            //return (IGenaricRepository<TEntity, Tkey>)_repositoryies[type];


           return (IGenaricRepository<TEntity, Tkey>) _repositoryies.GetOrAdd(typeof(TEntity).Name, new GenaricRepository<TEntity, Tkey>(_context));


        }

        public async Task<int> SaveChangesAsync()
        {
           return await _context.SaveChangesAsync();
        }
    }
}
