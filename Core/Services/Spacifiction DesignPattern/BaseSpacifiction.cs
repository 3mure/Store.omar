using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Contract;
using Domain.Models;

namespace Services.Spacifiction_DesignPattern
{
    public class BaseSpacifiction<Tentity, Tkey> : ISpacification<Tentity, Tkey> where Tentity : BaseEntity<Tkey>
    {
        public Expression<Func<Tentity, bool>> Criteria { get ; set; }
        public List<Expression<Func<Tentity, object>>> IncludeExpression { get; set; } = new List<Expression<Func<Tentity, object>>>();
        public Expression<Func<Tentity, object>> OrderBy { get ; set ; }
        public Expression<Func<Tentity, object>> OrderByDescending { get ; set ; }
        public int skip { get ; set ; }
        public int take { get ; set ; }
        public bool Ispagination { get; set ; }

        public BaseSpacifiction(Expression<Func<Tentity, bool>> expression)
        {
            Criteria = expression;
            //IncludeExpression = new List<Expression<Func<Tentity, object>>>(); // Initialize the list
        }
        protected void AddInclude(Expression<Func<Tentity, object>> expression) 
        {
         IncludeExpression.Add(expression);
        }
        protected void AddOrderBy(Expression<Func<Tentity, object>> expression) 
        {
            OrderBy = expression;
        }
        protected void AddOrderByDescending(Expression<Func<Tentity, object>> expression)
        {
            OrderByDescending = expression;
        }
        protected void ApplyPagination(int pageIndex, int pagesize)
        {
          
            Ispagination = true;
            skip = (pageIndex - 1) * pagesize;
            take = pagesize;
        }
    }
}
