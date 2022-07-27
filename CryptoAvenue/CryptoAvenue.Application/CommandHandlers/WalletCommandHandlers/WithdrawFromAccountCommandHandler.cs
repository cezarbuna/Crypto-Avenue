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
    public class WithdrawFromAccountCommandHandler : IRequestHandler<WithdrawFromAccountCommand>
    {
        private readonly IWalletRepository walletRepository;
        private readonly ICoinRepository coinRepository;

        public WithdrawFromAccountCommandHandler(IWalletRepository walletRepository, ICoinRepository coinRepository)
        {
            this.walletRepository = walletRepository;
            this.coinRepository = coinRepository;
        }

        public async Task<Unit> Handle(WithdrawFromAccountCommand request, CancellationToken cancellationToken)
        {
            Wallet wallet = null;

            if (request.Option == 0)
                wallet = walletRepository.GetWalletBy(x => x.UserId == request.UserId && x.Coin.Abbreviation == "EUR");
            else if (request.Option == 1)
                wallet = walletRepository.GetWalletBy(x => x.UserId == request.UserId && x.Coin.Abbreviation == "USD");
            else
                throw new ArgumentOutOfRangeException("You can only choose 0 or 1 for withdrawal");

            if (wallet.CoinAmount < request.WithdrawnAmount)
                throw new ArgumentOutOfRangeException("Withdrawn amount is greater than the available amount.");
            else if(wallet.CoinAmount == request.WithdrawnAmount)
                walletRepository.Delete(wallet);
            else
            {
                wallet.CoinAmount -= request.WithdrawnAmount;
                walletRepository.Update(wallet);
            }

            walletRepository.SaveChanges();

            return await Task.FromResult(Unit.Value);
        }
    }
}
