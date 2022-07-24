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
    public class ApplyTradeOfferCommandHandler : IRequestHandler<ApplyTradeOfferCommand, TradeOffer>
    {
        private readonly ITradeOfferRepository tradeOfferRepository;
        private readonly IWalletRepository walletRepository;

        public ApplyTradeOfferCommandHandler(ITradeOfferRepository tradeOfferRepository, IWalletRepository walletRepository)
        {
            this.tradeOfferRepository = tradeOfferRepository;
            this.walletRepository = walletRepository;
        }

        public async Task<TradeOffer> Handle(ApplyTradeOfferCommand request, CancellationToken cancellationToken)
        {
            var tradeOffer = tradeOfferRepository.GetTradeOfferById(request.TradeOfferId);

            var senderSentCoinWallet = walletRepository.GetWalletBy(x => x.CoinId == tradeOffer.SentCoinId && x.UserId == tradeOffer.SenderId);
            var senderReceivedCoinWallet = walletRepository.GetWalletBy(x => x.CoinId == tradeOffer.ReceivedCoinId && x.UserId == tradeOffer.SenderId);
            var recipientSentCoinWallet = walletRepository.GetWalletBy(x => x.CoinId == tradeOffer.SentCoinId && x.UserId == tradeOffer.RecipientId);
            var recipientReceivedCoinWallet = walletRepository.GetWalletBy(x => x.CoinId == tradeOffer.ReceivedCoinId && x.UserId == tradeOffer.RecipientId);

            senderSentCoinWallet.CoinAmount -= tradeOffer.SentCoinAmount;
            if(senderSentCoinWallet.CoinAmount <= 0) 
                walletRepository.Delete(senderSentCoinWallet);
            walletRepository.Update(senderSentCoinWallet);

            recipientReceivedCoinWallet.CoinAmount += tradeOffer.ReceivedCoinAmount;
            walletRepository.Update(senderSentCoinWallet);

            if(senderReceivedCoinWallet != null && recipientReceivedCoinWallet != null)
            {
                senderReceivedCoinWallet.CoinAmount += tradeOffer.SentCoinAmount;
            }

            return await Task.FromResult(tradeOffer);
        }
    }
}
