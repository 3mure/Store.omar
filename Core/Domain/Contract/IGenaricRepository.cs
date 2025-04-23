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
        Task<IEnumerable<TEntity>>  GetAllAsync(bool trackChange = false);
       Task<TEntity> GetByIdAsync(Tkey id);
       
        void Update(TEntity entity);
        void Delete(TEntity entity);

        Task AddAsync(TEntity entity);


    }
}
