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
            var recipientReceivedCoinWallet = walletRepository.GetWalletBy(x => x.CoinId == tradeOffer.ReceivedCoinId && x.UserId == tradeOffer.RecipientId);

            senderSentCoinWallet.CoinAmount -= tradeOffer.SentCoinAmount;
            walletRepository.Update(senderSentCoinWallet);
            recipientReceivedCoinWallet.CoinAmount -= tradeOffer.ReceivedCoinAmount;
            walletRepository.Update(recipientReceivedCoinWallet);

            var senderReceivedCoinWallet = walletRepository.GetWalletBy(x => x.CoinId == tradeOffer.ReceivedCoinId && x.UserId == tradeOffer.SenderId);
            var recipientSentCoinWallet = walletRepository.GetWalletBy(x => x.CoinId == tradeOffer.SentCoinId && x.UserId == tradeOffer.RecipientId);

            if(senderReceivedCoinWallet != null && recipientSentCoinWallet != null)
            {
                senderReceivedCoinWallet.CoinAmount += tradeOffer.ReceivedCoinAmount;
                walletRepository.Update(senderReceivedCoinWallet);
                recipientSentCoinWallet.CoinAmount += tradeOffer.SentCoinAmount;
                walletRepository.Update(recipientSentCoinWallet);
            }
            else if(senderReceivedCoinWallet == null && recipientSentCoinWallet != null)
            {
                walletRepository.Insert(new Wallet
                {
                    CoinId = tradeOffer.ReceivedCoinId,
                    UserId = tradeOffer.SenderId,
                    CoinAmount = tradeOffer.ReceivedCoinAmount
                });

                recipientSentCoinWallet.CoinAmount += tradeOffer.SentCoinAmount;
                walletRepository.Update(recipientSentCoinWallet);
            }
            else if(senderReceivedCoinWallet != null && recipientSentCoinWallet == null)
            {
                senderReceivedCoinWallet.CoinAmount += tradeOffer.ReceivedCoinAmount;
                walletRepository.Update(senderReceivedCoinWallet);

                walletRepository.Insert(new Wallet
                {
                    CoinId = tradeOffer.SentCoinId,
                    UserId = tradeOffer.RecipientId,
                    CoinAmount = tradeOffer.SentCoinAmount
                });
            }
            else
            {
                walletRepository.Insert(new Wallet
                {
                    CoinId = tradeOffer.ReceivedCoinId,
                    UserId = tradeOffer.SenderId,
                    CoinAmount = tradeOffer.ReceivedCoinAmount
                });

                walletRepository.Insert(new Wallet
                {
                    CoinId = tradeOffer.SentCoinId,
                    UserId = tradeOffer.RecipientId,
                    CoinAmount = tradeOffer.SentCoinAmount
                });
            }

            walletRepository.SaveChanges();

            tradeOfferRepository.Delete(tradeOffer);
            tradeOfferRepository.SaveChanges();

            return await Task.FromResult(tradeOffer);
        }
    }
}
