using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contract;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Persistence
{
    public class SpacificationEvaluator
    {

        public static IQueryable<Tentity> GetQuery<Tentity,Tkey>( IQueryable<Tentity> inputeQuery ,
            ISpacification<Tentity,Tkey> spac) where Tentity : BaseEntity<Tkey>
        {
            
            var query = inputeQuery;

            if (spac.Criteria is not null)
                query = query.Where(spac.Criteria);
            if (spac.OrderBy is not null)
                query = query.OrderBy(spac.OrderBy);
            else if (spac.OrderByDescending is not null)
                query = query.OrderByDescending(spac.OrderByDescending);
            if (spac.Ispagination)
            {
                query = query.Skip(spac.skip).Take(spac.take);
            }

            if (spac.IncludeExpression is not null && spac.IncludeExpression.Any())
                query = spac.IncludeExpression.Aggregate(query, (current, includeExpression) => current.Include(includeExpression));

            return query;

        }

    }
}
