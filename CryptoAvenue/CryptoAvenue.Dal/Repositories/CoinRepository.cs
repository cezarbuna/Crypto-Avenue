using CryptoAvenue.Domain.IRepositories;
using CryptoAvenue.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Dal.Repositories
{
    public class CoinRepository : GenericRepository<Coin>, ICoinRepository
    {
        public CoinRepository(CryptoAvenueDbContext context) : base(context)
        {
        }

        public IEnumerable<Coin> GetAllCoinsBy(Expression<Func<Coin, bool>> predicate)
        {
            return context.Coins.Where(predicate);
        }

        public Coin GetCoinBy(Expression<Func<Coin, bool>> predicate)
        {
            return context.Coins.SingleOrDefault(predicate);
        }

        public Coin GetCoinById(Guid coinId)
        {
            return context.Coins.SingleOrDefault(x => x.Id == coinId);
        }
    }
}
