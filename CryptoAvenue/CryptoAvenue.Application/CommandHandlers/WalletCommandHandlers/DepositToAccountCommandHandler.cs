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
    public class DepositToAccountCommandHandler : IRequestHandler<DepositToAccountCommand, Wallet>
    {
        private readonly IWalletRepository walletRepository;
        private readonly ICoinRepository coinRepository;

        public DepositToAccountCommandHandler(
            IWalletRepository walletRepository,
            ICoinRepository coinRepository)
        {
            this.walletRepository = walletRepository;
            this.coinRepository = coinRepository;
        }

        public async Task<Wallet> Handle(DepositToAccountCommand request, CancellationToken cancellationToken)
        {
            if (request.Option == 0)
            {
                var wallet = walletRepository.GetWalletBy(x => x.UserId == request.UserId && x.Coin.Abbreviation == "EUR");

                if (wallet == null)
                {
                    var newWallet = new Wallet
                    {
                        UserId = request.UserId,
                        CoinId = coinRepository.GetCoinBy(x => x.Abbreviation == "EUR").Id,
                        CoinAmount = request.DepositedAmount
                    };

                    walletRepository.Insert(newWallet);
                    walletRepository.SaveChanges();

                    return await Task.FromResult(newWallet);
                }
                else
                {
                    wallet.CoinAmount += request.DepositedAmount;
                    walletRepository.Update(wallet);
                    walletRepository.SaveChanges();

                    return await Task.FromResult(wallet);
                }

            }
            else if (request.Option == 1)
            {
                var wallet = walletRepository.GetWalletBy(x => x.UserId == request.UserId && x.Coin.Abbreviation == "USD");

                if (wallet == null)
                {
                    var newWallet = new Wallet
                    {
                        UserId = request.UserId,
                        CoinId = coinRepository.GetCoinBy(x => x.Abbreviation == "USD").Id,
                        CoinAmount = request.DepositedAmount
                    };

                    walletRepository.Insert(newWallet);
                    walletRepository.SaveChanges();

                    return await Task.FromResult(newWallet);
                }
                else
                {
                    wallet.CoinAmount += request.DepositedAmount;
                    walletRepository.Update(wallet);
                    walletRepository.SaveChanges();

                    return await Task.FromResult(wallet);
                }

            }
            else throw new ArgumentOutOfRangeException("You can only choose 0 or 1 for depositing");
        }
    }
}
