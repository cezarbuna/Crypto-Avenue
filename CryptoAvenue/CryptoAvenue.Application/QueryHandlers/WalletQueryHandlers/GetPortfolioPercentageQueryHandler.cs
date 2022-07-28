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
            /*var amounts = new List<double>();
            var result = new Dictionary<double, Coin>();

            foreach (var wallet in wallets)
            {
                amounts.Add(wallet.CoinAmount);
                result.Add(0, wallet.Coin);
            }

            double sum = amounts.Sum();
            var percentages = new List<double>();

            foreach (var amount in amounts)
            {
                percentages.Add((amount / sum) * 100);
            }

            for(int i = 0; i < percentages.Count; i++)
            {
                result[percentages.ElementAt(i)] = percentages[i]
            }*/

            throw new NotImplementedException("asd");
        }
    }
}
