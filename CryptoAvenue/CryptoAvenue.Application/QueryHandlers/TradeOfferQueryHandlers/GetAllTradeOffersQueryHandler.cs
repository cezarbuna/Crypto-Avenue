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
    public class GetAllTradeOffersQueryHandler : IRequestHandler<GetAllTradeOffersQuery, List<TradeOffer>>
    {
        private readonly ITradeOfferRepository repository;

        public GetAllTradeOffersQueryHandler(ITradeOfferRepository repository)
        {
            this.repository = repository;
        }

        public async Task<List<TradeOffer>> Handle(GetAllTradeOffersQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(repository.GetAllTradeOffersBy().ToList());
        }
    }
}
