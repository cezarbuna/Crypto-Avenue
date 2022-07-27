using CryptoAvenue.Application.Commands.TradeOfferCommands;
using CryptoAvenue.Domain.IRepositories;
using CryptoAvenue.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Application.CommandHandlers.TradeOfferCommandHandlers
{
    public class CreateTradeOfferCommandHandler : IRequestHandler<CreateTradeOfferCommand, TradeOffer>
    {
        private readonly ITradeOfferRepository tradeOfferRepository;
        private readonly ICoinRepository coinRepository;

        public CreateTradeOfferCommandHandler(ITradeOfferRepository tradeOfferRepository, ICoinRepository coinRepository)
        {
            this.tradeOfferRepository = tradeOfferRepository;
            this.coinRepository = coinRepository;
        }

        public async Task<TradeOffer> Handle(CreateTradeOfferCommand request, CancellationToken cancellationToken)
        {
            var sentCoin = coinRepository.GetCoinById(request.SentCoinId);
            var receivedCoin = coinRepository.GetCoinById(request.ReceivedCoinId);

            double sentAmount = (request.ReceivedCoinAmount * receivedCoin.ValueInEUR) / sentCoin.ValueInEUR;

            var newTradeOffer = new TradeOffer
            {
                SenderId = request.SenderId,
                RecipientId = request.RecipientId,
                SentCoinId = request.SentCoinId,
                ReceivedCoinId = request.ReceivedCoinId,
                SentCoinAmount = sentAmount,
                ReceivedCoinAmount = request.ReceivedCoinAmount
            };

            tradeOfferRepository.Insert(newTradeOffer);
            tradeOfferRepository.SaveChanges();

            return await Task.FromResult(newTradeOffer);
        }
    }
}
