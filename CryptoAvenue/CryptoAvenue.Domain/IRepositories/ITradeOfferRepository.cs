using CryptoAvenue.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Domain.IRepositories
{
    public interface ITradeOfferRepository : IGenericRepository<TradeOffer>
    {
        TradeOffer GetTradeOfferBy(Expression<Func<TradeOffer, bool>> predicate);
        TradeOffer GetTradeOfferById(Guid id);
        IEnumerable<TradeOffer> GetAllTradeOffersBy(Expression<Func<TradeOffer, bool>>? predicate = null);
    }
}
