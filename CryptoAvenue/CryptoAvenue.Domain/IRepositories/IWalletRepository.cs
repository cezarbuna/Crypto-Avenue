using CryptoAvenue.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Domain.IRepositories
{
    public interface IWalletRepository : IGenericRepository<Wallet>
    {
        Wallet GetWalletBy(Expression<Func<Wallet, bool>> predicate);
        Wallet GetWalletById(Guid id);
        IEnumerable<Wallet> GetAllWalletsBy(Expression<Func<Wallet, bool>>? predicate = null);
    }
}
