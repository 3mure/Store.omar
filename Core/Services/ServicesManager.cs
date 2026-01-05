using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contract;
using Services.Abstractions;

namespace Services
{
    public class ServicesManager(IUnitOfWork unitOfWork ,
        IMapper mapper,
        IBasketRepository basketRepository,
        ICacheRepository cacheRepository
        ) : IServicesManager
    {
        public IProductServices productServices { get; } = new ProductService(unitOfWork,mapper);

        public IBasketServices basketServices =>  new BasketService(basketRepository,mapper);

        public ICacheService cacheService => new CacheService(cacheRepository);
    }
}
