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
    public class TradeOfferRepository : GenericRepository<TradeOffer>, ITradeOfferRepository
    {
        public TradeOfferRepository(CryptoAvenueDbContext context) : base(context)
        {
        }

        public IEnumerable<TradeOffer> GetAllTradeOffersBy(Expression<Func<TradeOffer, bool>>? predicate = null)
        {
            if(predicate == null)
                return context.TradeOffers
                .Include(x => x.SentCoin)
                .Include(x => x.ReceivedCoin)
                .Include(x => x.Sender)
                .Include(x => x.Recipient);

            return context.TradeOffers.Where(predicate)
                .Include(x => x.SentCoin)
                .Include(x => x.ReceivedCoin)
                .Include(x => x.Sender)
                .Include(x => x.Recipient);
        }

        public TradeOffer GetTradeOfferBy(Expression<Func<TradeOffer, bool>> predicate)
        {
            return context.TradeOffers
                .Include(x => x.SentCoin)
                .Include(x => x.ReceivedCoin)
                .Include(x => x.Sender)
                .Include(x => x.Recipient)
                .SingleOrDefault(predicate);
        }

        public TradeOffer GetTradeOfferById(Guid id)
        {
            return context.TradeOffers
                .Include(x => x.SentCoin)
                .Include(x => x.ReceivedCoin)
                .Include(x => x.Sender)
                .Include(x => x.Recipient)
                .SingleOrDefault(x => x.Id == id);
        }
    }
}
