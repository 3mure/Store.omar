using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Contract
{
    public interface ISpacification<Tentity , Tkey> where Tentity : BaseEntity<Tkey>
    {
      public Expression<Func<Tentity, bool>> Criteria { get; set; }
        public List<Expression<Func<Tentity, object>>> IncludeExpression { get; set; }
        public Expression<Func<Tentity,object>> OrderBy { get; set; }
        public Expression<Func<Tentity,object>> OrderByDescending { get; set; }
        int skip { get; set; }
        int take { get; set; }
        bool Ispagination { get; set; }


    }
}
