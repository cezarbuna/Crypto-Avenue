using CryptoAvenue.Application.Queries.WalletQueries;
using CryptoAvenue.Domain.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Application.QueryHandlers.WalletQueryHandlers
{
    public class GetTotalPortfolioValueQueryHandler : IRequestHandler<GetTotalPortfolioValueQuery, double>
    {
        private readonly IWalletRepository repository;

        public GetTotalPortfolioValueQueryHandler(IWalletRepository repository)
        {
            this.repository = repository;
        }

        public async Task<double> Handle(GetTotalPortfolioValueQuery request, CancellationToken cancellationToken)
        {
            double totalPortfolioValue = 0;
            var wallets = repository.GetAllWalletsBy(x => x.UserId == request.UserId);

            if (request.Option == 0)
            {
                foreach (var wallet in wallets)
                {
                    totalPortfolioValue += wallet.Coin.ValueInEUR * wallet.CoinAmount;
                }
            }
            else if (request.Option == 1)
            {
                foreach (var wallet in wallets)
                {
                    totalPortfolioValue += wallet.Coin.ValueInUSD * wallet.CoinAmount;
                }
            }
            else throw new ArgumentOutOfRangeException("Option can only be 0 or 1");

            return await Task.FromResult(totalPortfolioValue);
        }
    }
}
