using CryptoAvenue.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Domain.IRepositories
{
    public interface ICoinRepository : IGenericRepository<Coin>
    {
        Coin GetCoinBy(Expression<Func<Coin, bool>> predicate);
        Coin GetCoinById(Guid coinId);
        IEnumerable<Coin> GetAllCoinsBy(Expression<Func<Coin, bool>> predicate);
    }
}
