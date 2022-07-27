﻿using CryptoAvenue.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Application.Queries.TradeOfferQueries
{
    public class GetAllTradeOffersByRecipientIdQuery : IRequest<List<TradeOffer>>
    {
        public Guid RecipientId { get; set; }
    }
}
