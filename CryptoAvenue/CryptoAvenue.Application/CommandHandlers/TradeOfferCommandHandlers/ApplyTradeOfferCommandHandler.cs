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

            var senderSentCoinWallet = walletRepository.GetWalletBy(x => x.UserId == tradeOffer.SenderId && x.CoinId == tradeOffer.SentCoinId);
            var senderReceivedCoinWallet = walletRepository.GetWalletBy(x => x.UserId == tradeOffer.SenderId && x.CoinId == tradeOffer.ReceivedCoinId);

            var recipientSentCoinWallet = walletRepository.GetWalletBy(x => x.UserId == tradeOffer.RecipientId && x.CoinId == tradeOffer.SentCoinId);
            var recipientReceivedCoinWallet = walletRepository.GetWalletBy(x => x.UserId == tradeOffer.RecipientId && x.CoinId == tradeOffer.ReceivedCoinId);

            senderSentCoinWallet.CoinAmount -= tradeOffer.SentCoinAmount;
            walletRepository.Update(senderSentCoinWallet);

            if (senderSentCoinWallet.CoinAmount <= 0)
                walletRepository.Delete(senderSentCoinWallet);

            recipientReceivedCoinWallet.CoinAmount -= tradeOffer.ReceivedCoinAmount;
            walletRepository.Update(recipientReceivedCoinWallet);

            if (recipientReceivedCoinWallet.CoinAmount <= 0)
                walletRepository.Delete(recipientReceivedCoinWallet);

            //case 1 -> if both wallets exist
            if(senderReceivedCoinWallet != null && recipientSentCoinWallet != null)
            {
                senderReceivedCoinWallet.CoinAmount += tradeOffer.ReceivedCoinAmount;
                walletRepository.Update(senderReceivedCoinWallet);

                recipientSentCoinWallet.CoinAmount += tradeOffer.SentCoinAmount;
                walletRepository.Update(recipientSentCoinWallet);
            }
            //case 2 -> if wallet 1 is null and wallet 2 exists
            else if(senderReceivedCoinWallet == null && recipientSentCoinWallet != null)
            {
                var newSenderReceivedCoinWallet = new Wallet
                {
                    CoinId = tradeOffer.ReceivedCoinId,
                    UserId = tradeOffer.SenderId,
                    CoinAmount = tradeOffer.ReceivedCoinAmount
                };
                walletRepository.Insert(newSenderReceivedCoinWallet);

                recipientSentCoinWallet.CoinAmount += tradeOffer.SentCoinAmount;
                walletRepository.Update(recipientSentCoinWallet);
            }
            //case 3 -> if wallet 1 exists and wallet 2 is null
            else if(senderReceivedCoinWallet != null && recipientSentCoinWallet == null)
            {
                senderReceivedCoinWallet.CoinAmount += tradeOffer.ReceivedCoinAmount;
                walletRepository.Update(senderReceivedCoinWallet);

                var newRecipientSentCoinWallet = new Wallet
                {
                    CoinId = tradeOffer.SentCoinId,
                    UserId = tradeOffer.RecipientId,
                    CoinAmount = tradeOffer.SentCoinAmount
                };
                walletRepository.Insert(newRecipientSentCoinWallet);
            }
            //case 4 -> if both wallets are null
            else
            {
                var newSenderReceivedCoinWallet = new Wallet
                {
                    CoinId = tradeOffer.ReceivedCoinId,
                    UserId = tradeOffer.SenderId,
                    CoinAmount = tradeOffer.ReceivedCoinAmount
                };
                walletRepository.Insert(newSenderReceivedCoinWallet);

                var newRecipientSentCoinWallet = new Wallet
                {
                    CoinId = tradeOffer.SentCoinId,
                    UserId = tradeOffer.RecipientId,
                    CoinAmount = tradeOffer.SentCoinAmount
                };
                walletRepository.Insert(newRecipientSentCoinWallet);
            }

            walletRepository.SaveChanges();

            tradeOfferRepository.Delete(tradeOffer);
            tradeOfferRepository.SaveChanges();

            return await Task.FromResult(tradeOffer);
        }
    }
}
