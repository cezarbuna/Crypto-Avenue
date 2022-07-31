using CryptoAvenue.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Application.Commands.TradeOfferCommands
{
    public class ApplyTradeOfferCommand : IRequest
    {
        public Guid TradeOfferId { get; set; }
    }
}
