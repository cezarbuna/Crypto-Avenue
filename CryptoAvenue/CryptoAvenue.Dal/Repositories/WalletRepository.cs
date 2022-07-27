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
    public class WalletRepository : GenericRepository<Wallet>, IWalletRepository
    {
        public WalletRepository(CryptoAvenueDbContext context) : base(context)
        {
        }

        public IEnumerable<Wallet> GetAllWalletsBy(Expression<Func<Wallet, bool>>? predicate = null)
        {
            if (predicate == null)
                return context.Wallets
                    .Include(x => x.Coin)
                    .Include(x => x.User)
                    .ToList();
            else
            {
                return context.Wallets.Where(predicate)
                .Include(x => x.User)
                .Include(x => x.Coin);
            }
        }

        public Wallet GetWalletBy(Expression<Func<Wallet, bool>> predicate)
        {
            return context.Wallets
                .Include(x => x.User)
                .Include(x => x.Coin)
                .SingleOrDefault(predicate);
        }

        public Wallet GetWalletById(Guid id)
        {
            return context.Wallets
                .Include(x => x.User)
                .Include(x => x.Coin)
                .SingleOrDefault(x => x.Id == id);
        }
    }
}
