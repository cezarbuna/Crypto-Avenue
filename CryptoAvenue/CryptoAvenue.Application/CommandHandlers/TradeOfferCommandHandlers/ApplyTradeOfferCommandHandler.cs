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
    public class ApplyTradeOfferCommandHandler : IRequestHandler<ApplyTradeOfferCommand>
    {
        private readonly ITradeOfferRepository tradeOfferRepository;
        private readonly IWalletRepository walletRepository;

        public ApplyTradeOfferCommandHandler(ITradeOfferRepository tradeOfferRepository, IWalletRepository walletRepository)
        {
            this.tradeOfferRepository = tradeOfferRepository;
            this.walletRepository = walletRepository;
        }

        public async Task<Unit> Handle(ApplyTradeOfferCommand request, CancellationToken cancellationToken)
        {
            var tradeOffer = tradeOfferRepository.GetTradeOfferById(request.TradeOfferId);

            throw new NotImplementedException("");
        }
    }
}
