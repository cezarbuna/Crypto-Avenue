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
    public class ConvertCoinsInUserWalletsCommandHandler : IRequestHandler<ConvertCoinsInUserWalletsCommand>
    {
        private readonly IWalletRepository walletRepository;
        private readonly ICoinRepository coinRepository;
        private readonly IUserRepository userRepository;

        public ConvertCoinsInUserWalletsCommandHandler(
            IWalletRepository walletRepository,
            ICoinRepository coinRepository,
            IUserRepository userRepository)
        {
            this.walletRepository = walletRepository;
            this.coinRepository = coinRepository;
            this.userRepository = userRepository;
        }

        public async Task<Unit> Handle(ConvertCoinsInUserWalletsCommand request, CancellationToken cancellationToken)
        {
            if (!walletRepository.Any(x => x.UserId == request.UserId && x.CoinId == request.SoldCoinId))
                throw new ArgumentException("The user does not own any sold coin");

            var soldCoinWallet = walletRepository.GetWalletBy(x => x.UserId == request.UserId && x.CoinId == request.SoldCoinId);

            var soldCoin = coinRepository.GetCoinById(request.SoldCoinId);
            var boughtCoin = coinRepository.GetCoinById(request.BoughtCoinId);

            var soldAmount = (request.BoughtAmount * boughtCoin.ValueInEUR) / soldCoin.ValueInEUR;

            //var boughtCoinWallet = walletRepository.GetWalletBy(x => x.UserId == request.UserId && x.CoinId == request.SoldCoinId);

            if (soldCoinWallet.CoinAmount < soldAmount)
                throw new Exception("Amount necessary for trade excedes available amount in the wallet.");


            return await Task.FromResult(Unit.Value);
        }
    }
}
