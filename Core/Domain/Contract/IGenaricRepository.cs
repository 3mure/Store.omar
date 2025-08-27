using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Contract
{
    public interface IGenaricRepository<TEntity , Tkey> where TEntity : BaseEntity<Tkey>
    {
        Task<int> CountAsync(ISpacification<TEntity,Tkey> spec);
        Task<IEnumerable<TEntity>>  GetAllAsync(bool trackChange = false);
        Task<IEnumerable<TEntity>> GetAllAsync(ISpacification<TEntity,Tkey> spec,bool trackChange = false);
        Task<TEntity> GetByIdAsync(Tkey id);
        Task<TEntity> GetByIdAsync(ISpacification<TEntity,Tkey> spec);

        void Update(TEntity entity);
        void Delete(TEntity entity);

        Task AddAsync(TEntity entity);


    }
}
