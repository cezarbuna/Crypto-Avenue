using CryptoAvenue.Application.Queries.WalletQueries;
using CryptoAvenue.Domain.IRepositories;
using CryptoAvenue.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Application.QueryHandlers.WalletQueryHandlers
{
    public class GetPortfolioPercentageQueryHandler : IRequestHandler<GetPortfolioPercentageQuery, Dictionary<double, Coin>>
    {
        private readonly IWalletRepository walletRepository;

        public GetPortfolioPercentageQueryHandler(IWalletRepository walletRepository)
        {
            this.walletRepository = walletRepository;
        }

        public async Task<Dictionary<double, Coin>> Handle(GetPortfolioPercentageQuery request, CancellationToken cancellationToken)
        {
            var wallets = walletRepository.GetAllWalletsBy(x => x.UserId == request.UserId);
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

            var result = new Dictionary<double, Coin>();

            for(int i = 0; i < percentages.Count; i++)
            {
                result.Add(percentages.ElementAt(i), wallets.ElementAt(i).Coin);
            }

            return await Task.FromResult(result);
        }
    }
}
