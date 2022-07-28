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
    public class ConvertCoinsInUserWalletsCommandHandler : IRequestHandler<ConvertCoinsInUserWalletsCommand, Wallet>
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

        public async Task<Wallet> Handle(ConvertCoinsInUserWalletsCommand request, CancellationToken cancellationToken)
        {
            var soldCoinWallet = walletRepository.GetWalletBy(x => x.CoinId == request.SoldCoinId && x.UserId == request.UserId);
            var soldCoin = coinRepository.GetCoinById(request.SoldCoinId);
            var boughtCoin = coinRepository.GetCoinById(request.BoughtCoinId);

            var soldAmount = (request.BoughtAmount * boughtCoin.ValueInEUR) / soldCoin.ValueInEUR;

            soldCoinWallet.CoinAmount -= soldAmount;
            
            if(walletRepository.Any(x => x.CoinId == request.BoughtCoinId))
            {
                var boughtCoinWallet = walletRepository.GetWalletBy(x => x.CoinId == request.BoughtCoinId);
                boughtCoinWallet.CoinAmount += request.BoughtAmount;
                walletRepository.Update(boughtCoinWallet);
                walletRepository.SaveChanges();

                return await Task.FromResult(boughtCoinWallet);
            }
            else
            {
                var boughtCoinWallet = new Wallet
                {
                    CoinId = request.BoughtCoinId,
                    UserId = request.UserId,
                    CoinAmount = request.BoughtAmount
                };

                walletRepository.Insert(boughtCoinWallet);
                walletRepository.SaveChanges();

                return await Task.FromResult(boughtCoinWallet);
            }
        }
    }
}
