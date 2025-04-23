using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contract;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repository
{
    public class GenaricRepository<TEntity, Tkey>  : IGenaricRepository<TEntity, Tkey> where TEntity:BaseEntity<Tkey>
    {
        private readonly StoreDbcontext _context;

        public GenaricRepository(StoreDbcontext context)
        {
           _context = context;
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool trackChange = false)
        {
            //if (trackChange)
            //{
            //    return await _context.Set<TEntity>().ToListAsync();
            //}
            //return await _context.Set<TEntity>().AsNoTracking().ToListAsync();
            if (typeof(TEntity)== typeof(Product)) 
            {
                return trackChange ?
                     await _context.Set<Product>().Include(P=>P.ProductBrand).Include(p=>p.ProductType).ToListAsync() as IEnumerable<TEntity>
                   : await _context.Set<Product>().Include(P => P.ProductBrand).Include(p => p.ProductType).AsNoTracking().ToListAsync() as IEnumerable<TEntity> ;
            }

            return  trackChange ?
                  await _context.Set<TEntity>().ToListAsync()
                : await _context.Set<TEntity>().AsNoTracking().ToListAsync();

        }

        public async Task<TEntity> GetByIdAsync(Tkey id)
        {
            if (typeof(TEntity) == typeof(Product))
            {
                return await _context.Set<Product>().Include(P => P.ProductBrand).Include(p => p.ProductType).FirstOrDefaultAsync(p=>p.Id == id as int? ) as TEntity;
            }
            return await _context.Set<TEntity>().FindAsync(id);
        }
        public async Task AddAsync(TEntity entity)
        {
             await _context.Set<TEntity>().AddAsync(entity);
        }
        public void Update(TEntity entity)
        {
             _context.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

       

       
       
    }
}
