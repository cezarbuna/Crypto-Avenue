using CryptoAvenue.Application.Queries.TradeOfferQueries;
using CryptoAvenue.Domain.IRepositories;
using CryptoAvenue.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Application.QueryHandlers.TradeOfferQueryHandlers
{
    public class GetAllTradeOffersByRecipientIdQueryHandler : IRequestHandler<GetAllTradeOffersByRecipientIdQuery, List<TradeOffer>>
    {
        private readonly ITradeOfferRepository repository;

        public GetAllTradeOffersByRecipientIdQueryHandler(ITradeOfferRepository repository)
        {
            this.repository = repository;
        }

        public async Task<List<TradeOffer>> Handle(GetAllTradeOffersByRecipientIdQuery request, CancellationToken cancellationToken)
        {
            var foundTradeOffers = repository.GetAllTradeOffersBy(x => x.RecipientId == request.RecipientId).ToList();
            return await Task.FromResult(foundTradeOffers);
        }
    }
}
