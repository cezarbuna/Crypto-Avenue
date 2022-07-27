﻿using CryptoAvenue.Application.Queries.TradeOfferQueries;
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
    public class GetTradeOfferByIdQueryHandler : IRequestHandler<GetTradeOfferByIdQuery, TradeOffer>
    {
        private readonly ITradeOfferRepository repository;

        public GetTradeOfferByIdQueryHandler(ITradeOfferRepository repository)
        {
            this.repository = repository;
        }

        public async Task<TradeOffer> Handle(GetTradeOfferByIdQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(repository.GetTradeOfferById(request.TradeOfferId));
        }
    }
}
