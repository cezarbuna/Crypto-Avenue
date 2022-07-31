using CryptoAvenue.Application.Commands.WalletCommands;
using CryptoAvenue.Domain.IRepositories;
using CryptoAvenue.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Application.CommandHandlers.WalletCommandHandlers
{
    public class CopyUserPortfolioCommandHandler : IRequestHandler<CopyUserPortfolioCommand>
    {
        private readonly IWalletRepository walletRepository;
        private readonly ICoinRepository coinRepository;

        public CopyUserPortfolioCommandHandler(IWalletRepository walletRepository, ICoinRepository coinRepository)
        {
            this.walletRepository = walletRepository;
            this.coinRepository = coinRepository;
        }

        public async Task<Unit> Handle(CopyUserPortfolioCommand request, CancellationToken cancellationToken)
        {
            string chosenCurrency = "";

            if (request.Option == 0)
                chosenCurrency = "EUR";
            else if (request.Option == 1)
                chosenCurrency = "USD";
            else
                throw new ArgumentOutOfRangeException("You must choose 0 or 1 for the command to work.");

            var chosenCurrencyWallet = walletRepository.GetWalletBy(x => x.UserId == request.CopiedUserId && x.Coin.Abbreviation == chosenCurrency);

            var wallets = walletRepository.GetAllWalletsBy(x => x.UserId == request.CopiedUserId);
            var pasterWallets = walletRepository.GetAllWalletsBy(x => x.UserId == request.PastedUserId);
            var amountsInEur = new List<double>();
            double sumInEur = 0;

            foreach (var wallet in wallets)
            {
                amountsInEur.Add(wallet.CoinAmount * wallet.Coin.ValueInEUR);
                sumInEur += wallet.CoinAmount * wallet.Coin.ValueInEUR;
            }

            var percentages = new List<double>();

            foreach (var amount in amountsInEur)
            {
                percentages.Add(Math.Round((amount / sumInEur) * 100, 3));
            }

            var percentageCoin = new Dictionary<double, Coin>();

            for (int i = 0; i < percentages.Count; i++)
            {
                percentageCoin.Add(percentages.ElementAt(i), wallets.ElementAt(i).Coin);
            }

            int copiedAmount = request.Amount;

            foreach (var pair in percentageCoin)
            {
                var wallet = pasterWallets.SingleOrDefault(x => x.Coin == pair.Value);
                var copiedPercentageFromTotal = (pair.Key / 100) * copiedAmount;
                var copiedAmountOfCoin =
                    (copiedPercentageFromTotal * chosenCurrencyWallet.Coin.ValueInEUR)
                    / pair.Value.ValueInEUR;

                if(wallet != null)
                {
                    wallet.CoinAmount += copiedAmountOfCoin;
                    walletRepository.Update(wallet);
                }
                else
                {
                    walletRepository.Insert(new Wallet
                    {
                        UserId = request.PastedUserId,
                        CoinId = pair.Value.Id,
                        CoinAmount = copiedAmountOfCoin
                    });
                }
            }

            walletRepository.SaveChanges();

            return await Task.FromResult(Unit.Value);
        }
    }
}
